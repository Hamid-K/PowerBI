using System;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200001D RID: 29
	public sealed class ReportSize
	{
		// Token: 0x060003BC RID: 956 RVA: 0x00009979 File Offset: 0x00007B79
		public ReportSize(string size)
		{
			this.m_size = size;
			this.Validate();
			this.m_parsed = true;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00009995 File Offset: 0x00007B95
		internal ReportSize(string size, double sizeInMM)
		{
			this.m_size = size;
			this.m_sizeInMM = sizeInMM;
			this.m_parsed = true;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000099B2 File Offset: 0x00007BB2
		internal ReportSize(ReportSize newSize)
		{
			this.m_size = newSize.ToString();
			this.m_sizeInMM = newSize.ToMillimeters();
			this.m_parsed = true;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000099D9 File Offset: 0x00007BD9
		internal ReportSize(string size, bool parsed)
		{
			this.m_size = size;
			this.m_parsed = parsed;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000099EF File Offset: 0x00007BEF
		private ReportSize()
		{
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000099F7 File Offset: 0x00007BF7
		public override string ToString()
		{
			return this.m_size;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000099FF File Offset: 0x00007BFF
		public double ToMillimeters()
		{
			this.ParseSize();
			return this.m_sizeInMM;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00009A0D File Offset: 0x00007C0D
		public double ToInches()
		{
			this.ParseSize();
			return this.m_sizeInMM / 25.4;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00009A25 File Offset: 0x00007C25
		public double ToPoints()
		{
			this.ParseSize();
			return this.m_sizeInMM / 0.3528;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00009A3D File Offset: 0x00007C3D
		public double ToCentimeters()
		{
			this.ParseSize();
			return this.m_sizeInMM / 10.0;
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00009A55 File Offset: 0x00007C55
		internal bool Parsed
		{
			get
			{
				return this.m_parsed;
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00009A5D File Offset: 0x00007C5D
		internal void ParseSize()
		{
			if (!this.m_parsed)
			{
				Validator.ParseSize(this.m_size, out this.m_sizeInMM);
				this.m_parsed = true;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00009A80 File Offset: 0x00007C80
		internal void Validate()
		{
			RVUnit rvunit;
			if (!Validator.ValidateSizeString(this.m_size, out rvunit))
			{
				throw new ReportRenderingException(ErrorCode.rrInvalidSize, new object[] { this.m_size });
			}
			if (!Validator.ValidateSizeUnitType(rvunit))
			{
				throw new ReportRenderingException(ErrorCode.rrInvalidMeasurementUnit, new object[] { this.m_size });
			}
			if (!Validator.ValidateSizeIsPositive(rvunit))
			{
				throw new ReportRenderingException(ErrorCode.rrNegativeSize, new object[] { this.m_size });
			}
			double num = Converter.ConvertToMM(rvunit);
			if (!Validator.ValidateSizeValue(num, Validator.NormalMin, Validator.NormalMax))
			{
				throw new ReportRenderingException(ErrorCode.rrOutOfRange, new object[] { this.m_size });
			}
			this.m_sizeInMM = num;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00009B34 File Offset: 0x00007D34
		internal ReportSize DeepClone()
		{
			ReportSize reportSize = new ReportSize();
			if (this.m_size != null)
			{
				reportSize.m_size = string.Copy(this.m_size);
			}
			reportSize.m_parsed = this.m_parsed;
			reportSize.m_sizeInMM = this.m_sizeInMM;
			return reportSize;
		}

		// Token: 0x04000075 RID: 117
		private string m_size;

		// Token: 0x04000076 RID: 118
		private double m_sizeInMM;

		// Token: 0x04000077 RID: 119
		private bool m_parsed;
	}
}
