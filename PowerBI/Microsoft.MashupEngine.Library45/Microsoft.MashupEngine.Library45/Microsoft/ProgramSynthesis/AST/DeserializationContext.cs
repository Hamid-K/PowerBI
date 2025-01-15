using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008CA RID: 2250
	public struct DeserializationContext : IEquatable<DeserializationContext>
	{
		// Token: 0x0600305A RID: 12378 RVA: 0x0008E718 File Offset: 0x0008C918
		private DeserializationContext(IReadOnlyDictionary<Type, object> context)
		{
			foreach (KeyValuePair<Type, object> keyValuePair in context)
			{
				if (!keyValuePair.Key.IsInstanceOfType(keyValuePair.Value))
				{
					throw new ArgumentException("context", string.Format("Context has key typeof({0}) with value not of that type: {1}.", keyValuePair.Key.CsName(true), keyValuePair.Value));
				}
			}
			this._context = context;
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x0008E7A0 File Offset: 0x0008C9A0
		public static DeserializationContext Create<T>(T context)
		{
			Dictionary<Type, object> dictionary = new Dictionary<Type, object>();
			Type typeFromHandle = typeof(T);
			dictionary[typeFromHandle] = context;
			return new DeserializationContext(dictionary);
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x0008E7CF File Offset: 0x0008C9CF
		public T Get<T>()
		{
			return (T)((object)this._context[typeof(T)]);
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x0008E7EB File Offset: 0x0008C9EB
		public Optional<T> MaybeGet<T>()
		{
			IReadOnlyDictionary<Type, object> context = this._context;
			return ((context != null) ? context.MaybeGet(typeof(T)) : Optional<object>.Nothing).Cast<T>();
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x0008E818 File Offset: 0x0008CA18
		public bool TryGetValue<T>(out T value)
		{
			if (this._context == null)
			{
				value = default(T);
				return false;
			}
			object obj;
			bool flag = this._context.TryGetValue(typeof(T), out obj);
			value = (flag ? ((T)((object)obj)) : default(T));
			return flag;
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x0008E869 File Offset: 0x0008CA69
		public bool Equals(DeserializationContext other)
		{
			return this._context == other._context;
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x0008E87C File Offset: 0x0008CA7C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is DeserializationContext)
			{
				DeserializationContext deserializationContext = (DeserializationContext)obj;
				return this.Equals(deserializationContext);
			}
			return false;
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x0008E8A6 File Offset: 0x0008CAA6
		public override int GetHashCode()
		{
			if (this._context == null)
			{
				return 0;
			}
			return this._context.GetHashCode();
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x0008E8BD File Offset: 0x0008CABD
		public static bool operator ==(DeserializationContext left, DeserializationContext right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x0008E8C7 File Offset: 0x0008CAC7
		public static bool operator !=(DeserializationContext left, DeserializationContext right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001856 RID: 6230
		private readonly IReadOnlyDictionary<Type, object> _context;
	}
}
