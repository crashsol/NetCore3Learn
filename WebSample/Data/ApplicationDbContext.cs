using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebSample.Models;

namespace WebSample.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }


        private static readonly MethodInfo ConfigureBasePropertiesMethodInfo
           = typeof(ApplicationDbContext)
               .GetMethod(
                   nameof(ConfigureBaseProperties),
                   BindingFlags.Instance | BindingFlags.NonPublic
               );


        /// <summary>
        /// 配置全局过滤器
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="mutableEntityType"></param>
        protected virtual void ConfigureBaseProperties<TEntity>(ModelBuilder modelBuilder, IMutableEntityType mutableEntityType)
           where TEntity : class
        {
            ConfigureGlobalFilters<TEntity>(modelBuilder, mutableEntityType);
        }


        /// <summary>
        /// 创建全局过滤器
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="mutableEntityType"></param>
        protected virtual void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType mutableEntityType)
          where TEntity : class
        {
            if (mutableEntityType.BaseType == null && ShouldFilterEntity<TEntity>(mutableEntityType))
            {
                
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                }
            }
        }

        /// <summary>
        /// 判断是否需要添加全局过滤参数
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entityType"></param>
        /// <returns></returns>
        protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        {
            if (typeof(IMultiTenant).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            return false;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
               // ConfigureBaseProperties<entityType.ClrType>(builder, entityType);
               ConfigureBasePropertiesMethodInfo.MakeGenericMethod(entityType.ClrType).Invoke(this, new object[] { builder, entityType });
            }
            base.OnModelCreating(builder);

        }

        /// <summary>
        /// 全局过滤器Express创建
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
         where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                /* This condition should normally be defined as below:
                 * !IsSoftDeleteFilterEnabled || !((ISoftDelete) e).IsDeleted
                 * But this causes a problem with EF Core (see https://github.com/aspnet/EntityFrameworkCore/issues/9502)
                 * So, we made a workaround to make it working. It works same as above.
                 */

                Expression<Func<TEntity, bool>> softDeleteFilter = e => !((ISoftDelete)e).IsDelete;
                expression = expression == null ? softDeleteFilter : CombineExpressions(expression, softDeleteFilter);
            }

            //if (typeof(IMultiTenant).IsAssignableFrom(typeof(TEntity)))
            //{
            //    /* This condition should normally be defined as below:
            //     * !IsMayHaveTenantFilterEnabled || ((IMayHaveTenant)e).TenantId == CurrentTenantId
            //     * But this causes a problem with EF Core (see https://github.com/aspnet/EntityFrameworkCore/issues/9502)
            //     * So, we made a workaround to make it working. It works same as above.
            //     */
            //    Expression<Func<TEntity, bool>> multiTenantFilter = e => ((IMultiTenant)e).TenantId == CurrentTenantId || (((IMultiTenant)e).TenantId == CurrentTenantId) == IsMultiTenantFilterEnabled;
            //    expression = expression == null ? multiTenantFilter : CombineExpressions(expression, multiTenantFilter);
            //}

            return expression;
        }

        protected virtual Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expression1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expression2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }


        class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                {
                    return _newValue;
                }

                return base.Visit(node);
            }
        }

    }
}
