using System;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002C3 RID: 707
	public sealed class ReportSize
	{
		// Token: 0x06001ABE RID: 6846 RVA: 0x0006B61C File Offset: 0x0006981C
		public ReportSize(string size)
			: this(size, true, false)
		{
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x0006B627 File Offset: 0x00069827
		public ReportSize(string size, bool allowNegative)
			: this(size, true, allowNegative)
		{
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0006B634 File Offset: 0x00069834
		internal ReportSize(string size, bool validate, bool allowNegative)
		{
			if (string.IsNullOrEmpty(size))
			{
				this.m_size = "0mm";
			}
			else
			{
				this.m_size = size;
			}
			this.m_allowNegative = allowNegative;
			if (validate)
			{
				this.Validate();
				this.m_parsed = true;
				return;
			}
			this.m_parsed = false;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0006B682 File Offset: 0x00069882
		internal ReportSize(string size, double sizeInMM)
		{
			this.m_sizeInMM = sizeInMM;
			this.m_parsed = true;
			if (string.IsNullOrEmpty(size))
			{
				this.m_size = ReportSize.ConvertToMM(this.m_sizeInMM);
				return;
			}
			this.m_size = size;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0006B6B9 File Offset: 0x000698B9
		internal ReportSize(ReportSize oldSize)
		{
			this.m_size = oldSize.ToString();
			this.m_parsed = oldSize.Parsed;
			if (this.m_parsed)
			{
				this.m_sizeInMM = oldSize.ToMillimeters();
			}
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0006B6ED File Offset: 0x000698ED
		private ReportSize()
		{
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0006B6F5 File Offset: 0x000698F5
		public override string ToString()
		{
			return this.m_size;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0006B6FD File Offset: 0x000698FD
		public double ToMillimeters()
		{
			this.ParseSize();
			return this.m_sizeInMM;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0006B70B File Offset: 0x0006990B
		public double ToInches()
		{
			this.ParseSize();
			return this.m_sizeInMM / 25.4;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0006B723 File Offset: 0x00069923
		public double ToPoints()
		{
			this.ParseSize();
			return this.m_sizeInMM / 0.35277777777777775;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0006B73B File Offset: 0x0006993B
		public double ToCentimeters()
		{
			this.ParseSize();
			return this.m_sizeInMM / 10.0;
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0006B753 File Offset: 0x00069953
		public static ReportSize SumSizes(ReportSize size1, ReportSize size2)
		{
			if (size1 == null)
			{
				return size2;
			}
			if (size2 == null)
			{
				return size1;
			}
			return ReportSize.FromMillimeters(size1.ToMillimeters() + size2.ToMillimeters());
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0006B771 File Offset: 0x00069971
		public static ReportSize FromMillimeters(double millimeters)
		{
			return new ReportSize(ReportSize.ConvertToMM(millimeters), millimeters);
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0006B77F File Offset: 0x0006997F
		private static string ConvertToMM(double millimeters)
		{
			return Convert.ToString(millimeters, CultureInfo.InvariantCulture) + "mm";
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0006B796 File Offset: 0x00069996
		internal void ParseSize()
		{
			if (!this.m_parsed)
			{
				Microsoft.ReportingServices.ReportPublishing.Validator.ParseSize(this.m_size, out this.m_sizeInMM);
				this.m_parsed = true;
			}
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0006B7B8 File Offset: 0x000699B8
		internal void Validate()
		{
			RVUnit rvunit;
			if (!Microsoft.ReportingServices.ReportPublishing.Validator.ValidateSizeString(this.m_size, out rvunit))
			{
				throw new RenderingObjectModelException(ErrorCode.rrInvalidSize, new object[] { this.m_size });
			}
			if (!Microsoft.ReportingServices.ReportPublishing.Validator.ValidateSizeUnitType(rvunit))
			{
				throw new RenderingObjectModelException(ErrorCode.rrInvalidMeasurementUnit, new object[] { this.m_size });
			}
			if (!this.m_allowNegative && !Microsoft.ReportingServices.ReportPublishing.Validator.ValidateSizeIsPositive(rvunit))
			{
				throw new RenderingObjectModelException(ErrorCode.rrNegativeSize, new object[] { this.m_size });
			}
			double num = Microsoft.ReportingServices.ReportPublishing.Converter.ConvertToMM(rvunit);
			if (!Microsoft.ReportingServices.ReportPublishing.Validator.ValidateSizeValue(num, this.m_allowNegative ? Microsoft.ReportingServices.ReportPublishing.Validator.NegativeMin : Microsoft.ReportingServices.ReportPublishing.Validator.NormalMin, Microsoft.ReportingServices.ReportPublishing.Validator.NormalMax))
			{
				throw new RenderingObjectModelException(ErrorCode.rrOutOfRange, new object[] { this.m_size });
			}
			this.m_sizeInMM = num;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0006B884 File Offset: 0x00069A84
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

		// Token: 0x06001ACF RID: 6863 RVA: 0x0006B8C9 File Offset: 0x00069AC9
		public static bool TryParse(string value, out ReportSize reportSize)
		{
			return ReportSize.TryParse(value, false, out reportSize);
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0006B8D4 File Offset: 0x00069AD4
		public static bool TryParse(string value, bool allowNegative, out ReportSize reportSize)
		{
			double num;
			if (Microsoft.ReportingServices.ReportPublishing.Validator.ValidateSize(value, allowNegative, out num))
			{
				reportSize = new ReportSize(value, num);
				return true;
			}
			reportSize = null;
			return false;
		}

		// Token: 0x04000D54 RID: 3412
		private const string m_zeroMM = "0mm";

		// Token: 0x04000D55 RID: 3413
		private string m_size;

		// Token: 0x04000D56 RID: 3414
		private double m_sizeInMM;

		// Token: 0x04000D57 RID: 3415
		private bool m_parsed;

		// Token: 0x04000D58 RID: 3416
		private bool m_allowNegative;
	}
}
