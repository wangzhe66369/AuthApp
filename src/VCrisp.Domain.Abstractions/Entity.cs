using System;
using System.Collections.Generic;
using System.Text;

namespace VCrisp.Domain.Abstractions
{
    /// <summary>
    /// 实体抽象类（包含多个主键的实体接口）
    /// </summary>
    public abstract class Entity : IEntity
    {
        public abstract object[] GetKeys();

        public override string ToString()
        {
            return $"[Entity:{GetType().Name}] Keys = {string.Join(",", GetKeys())}";
        }

    }

    /// <summary>
    /// 实体抽象类（包含唯一主键Id的实体接口）
    /// </summary>
    /// <typeparam name="TKey">主键ID类型</typeparam>
    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        int? _requestedHasCode;
        public virtual TKey Id { get; protected set; }
        public override object[] GetKeys()
        {
            return new object[] { Id };
        }

        /// <summary>
        /// 对象是否想等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TKey>))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            Entity<TKey> item = (Entity<TKey>)obj;

            if (item.IsTransient() || this.IsTransient())
            {
                return false;
            }
            else
            {
                return item.Id.Equals(this.Id);
            }
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHasCode.HasValue)
                {
                    _requestedHasCode = this.Id.GetHashCode() ^ 31; // TODO
                }
                return _requestedHasCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }

        /// <summary>
        /// 对象是否为全新创建的，未持久化的
        /// </summary>
        /// <returns></returns>
        public bool IsTransient()
        {
            return EqualityComparer<TKey>.Default.Equals(Id, default);
        }
        public override string ToString()
        {
            return $"[Entity:{GetType().Name}] Id = {Id}";
        }

        /// <summary>
        /// == 操作符重载
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Entity<TKey> left,Entity<TKey> right)
        {
            if (Object.Equals(left,null))
            {
                return (Object.Equals(right, null)) ? true : false;
            }
            else
            {
                return left.Equals(right);
            }
        }

        /// <summary>
        /// != 操作符重载
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
    }
}
