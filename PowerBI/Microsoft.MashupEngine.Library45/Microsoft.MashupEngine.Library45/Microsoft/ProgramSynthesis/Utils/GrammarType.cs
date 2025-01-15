using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000490 RID: 1168
	[DataContract]
	public abstract class GrammarType : IEquatable<GrammarType>
	{
		// Token: 0x06001A46 RID: 6726
		public abstract string CsName();

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001A47 RID: 6727
		public abstract Type Type { get; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x0004F494 File Offset: 0x0004D694
		public virtual bool? IsStatic
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x00002188 File Offset: 0x00000388
		public virtual Location Location
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x00002188 File Offset: 0x00000388
		public virtual string Name
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00002188 File Offset: 0x00000388
		public virtual MethodInfo GetMethod(string name, BindingFlags bindingFlags, Type[] parameterTypes)
		{
			return null;
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0004F4AA File Offset: 0x0004D6AA
		public virtual MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			return new MethodInfo[0];
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0004F4B2 File Offset: 0x0004D6B2
		public virtual MemberInfo[] GetMember(string name)
		{
			return new MemberInfo[0];
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0004F4BC File Offset: 0x0004D6BC
		internal static GrammarType MakeFunctionType(params GrammarType[] typeArguments)
		{
			if (typeArguments.All((GrammarType arg) => arg is ResolvedType))
			{
				Type[] array = (from arg in typeArguments.OfType<ResolvedType>()
					select arg.Type).ToArray<Type>();
				return new ResolvedType(typeof(Func<, >).MakeGenericType(array));
			}
			string text = "Func<{0}>";
			object[] array2 = new object[1];
			array2[0] = string.Join(", ", typeArguments.Select((GrammarType arg) => arg.CsName()));
			return new UnresolvedType(FormattableString.Invariant(FormattableStringFactory.Create(text, array2)));
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0004F582 File Offset: 0x0004D782
		public static bool operator ==(GrammarType type1, GrammarType type2)
		{
			if (type1 == null)
			{
				return type2 == null;
			}
			return type1.Equals(type2);
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0004F593 File Offset: 0x0004D793
		public static bool operator !=(GrammarType type1, GrammarType type2)
		{
			return !(type1 == type2);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0004F59F File Offset: 0x0004D79F
		public bool Equals(GrammarType other)
		{
			return other != null && this.Equals(other);
		}

		// Token: 0x06001A52 RID: 6738
		public abstract override bool Equals(object obj);

		// Token: 0x06001A53 RID: 6739
		public abstract override int GetHashCode();
	}
}
