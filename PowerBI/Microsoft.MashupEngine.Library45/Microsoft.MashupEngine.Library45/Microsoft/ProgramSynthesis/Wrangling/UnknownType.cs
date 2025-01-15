using System;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000D7 RID: 215
	public class UnknownType : IType, IEquatable<IType>, IEquatable<UnknownType>
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000105D2 File Offset: 0x0000E7D2
		public static IType Instance { get; } = new UnknownType();

		// Token: 0x060004BE RID: 1214 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Equals(UnknownType other)
		{
			return true;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool IsValidObject(ITypedValue obj)
		{
			return true;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000105D9 File Offset: 0x0000E7D9
		public bool IsAssignableFrom(IType other)
		{
			return other is UnknownType;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000105D9 File Offset: 0x0000E7D9
		public bool Equals(IType other)
		{
			return other is UnknownType;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000105E4 File Offset: 0x0000E7E4
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((UnknownType)obj)));
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00010612 File Offset: 0x0000E812
		public override int GetHashCode()
		{
			return 1500450271;
		}
	}
}
