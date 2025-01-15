using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006F5 RID: 1781
	[Serializable]
	internal sealed class MatrixRow
	{
		// Token: 0x170022E0 RID: 8928
		// (get) Token: 0x060062AE RID: 25262 RVA: 0x00189330 File Offset: 0x00187530
		// (set) Token: 0x060062AF RID: 25263 RVA: 0x00189338 File Offset: 0x00187538
		internal string Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		// Token: 0x170022E1 RID: 8929
		// (get) Token: 0x060062B0 RID: 25264 RVA: 0x00189341 File Offset: 0x00187541
		// (set) Token: 0x060062B1 RID: 25265 RVA: 0x00189349 File Offset: 0x00187549
		internal double HeightValue
		{
			get
			{
				return this.m_heightValue;
			}
			set
			{
				this.m_heightValue = value;
			}
		}

		// Token: 0x170022E2 RID: 8930
		// (get) Token: 0x060062B2 RID: 25266 RVA: 0x00189352 File Offset: 0x00187552
		// (set) Token: 0x060062B3 RID: 25267 RVA: 0x0018935A File Offset: 0x0018755A
		internal int NumberOfMatrixCells
		{
			get
			{
				return this.m_numberOfMatrixCells;
			}
			set
			{
				this.m_numberOfMatrixCells = value;
			}
		}

		// Token: 0x060062B4 RID: 25268 RVA: 0x00189363 File Offset: 0x00187563
		internal void Initialize(InitializationContext context)
		{
			this.m_heightValue = context.ValidateSize(ref this.m_height, "Height");
		}

		// Token: 0x060062B5 RID: 25269 RVA: 0x00189380 File Offset: 0x00187580
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Height, Token.String),
				new MemberInfo(MemberName.HeightValue, Token.Double)
			});
		}

		// Token: 0x040031C3 RID: 12739
		private string m_height;

		// Token: 0x040031C4 RID: 12740
		private double m_heightValue;

		// Token: 0x040031C5 RID: 12741
		[NonSerialized]
		private int m_numberOfMatrixCells;
	}
}
