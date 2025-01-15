using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012C1 RID: 4801
	internal class TypeAndCultureIdentity : IFunctionIdentity, IEquatable<IFunctionIdentity>
	{
		// Token: 0x06007E32 RID: 32306 RVA: 0x001B06CE File Offset: 0x001AE8CE
		public TypeAndCultureIdentity(Type type, ICulture culture)
			: this(type, (culture != null) ? culture.Name : null)
		{
		}

		// Token: 0x06007E33 RID: 32307 RVA: 0x001B06E3 File Offset: 0x001AE8E3
		public TypeAndCultureIdentity(Type type, string culture)
		{
			this.type = type;
			this.culture = culture;
		}

		// Token: 0x06007E34 RID: 32308 RVA: 0x001B06F9 File Offset: 0x001AE8F9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TypeAndCultureIdentity);
		}

		// Token: 0x06007E35 RID: 32309 RVA: 0x001B06F9 File Offset: 0x001AE8F9
		public bool Equals(IFunctionIdentity identity)
		{
			return this.Equals(identity as TypeAndCultureIdentity);
		}

		// Token: 0x06007E36 RID: 32310 RVA: 0x001B0707 File Offset: 0x001AE907
		public bool Equals(TypeAndCultureIdentity identity)
		{
			return identity != null && identity.type == this.type && identity.culture == this.culture;
		}

		// Token: 0x06007E37 RID: 32311 RVA: 0x001B0732 File Offset: 0x001AE932
		public override int GetHashCode()
		{
			int hashCode = this.type.GetHashCode();
			string text = this.culture;
			return hashCode + ((text != null) ? text.GetHashCode() : 0) * 17;
		}

		// Token: 0x0400454E RID: 17742
		private readonly Type type;

		// Token: 0x0400454F RID: 17743
		private readonly string culture;
	}
}
