using System;
using System.Diagnostics;
using System.Globalization;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003129 RID: 12585
	[DebuggerDisplay("RestrictionField={RestrictionField}")]
	[Serializable]
	internal abstract class SimpleTypeRestriction
	{
		// Token: 0x1700993E RID: 39230
		// (get) Token: 0x0601B4B5 RID: 111797 RVA: 0x00375DC3 File Offset: 0x00373FC3
		// (set) Token: 0x0601B4B6 RID: 111798 RVA: 0x00375DCB File Offset: 0x00373FCB
		internal FileFormatVersions FileFormat
		{
			get
			{
				return this._fileFormat;
			}
			set
			{
				this._fileFormat = value;
			}
		}

		// Token: 0x0601B4B7 RID: 111799 RVA: 0x00375DD4 File Offset: 0x00373FD4
		public SimpleTypeRestriction()
		{
			this.RestrictionField = RestrictionField.None;
		}

		// Token: 0x1700993F RID: 39231
		// (get) Token: 0x0601B4B8 RID: 111800
		// (set) Token: 0x0601B4B9 RID: 111801
		public abstract XsdType XsdType { get; set; }

		// Token: 0x17009940 RID: 39232
		// (get) Token: 0x0601B4BA RID: 111802
		public abstract string ClrTypeName { get; }

		// Token: 0x17009941 RID: 39233
		// (get) Token: 0x0601B4BB RID: 111803 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool IsEnum
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17009942 RID: 39234
		// (get) Token: 0x0601B4BC RID: 111804 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool IsList
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17009943 RID: 39235
		// (get) Token: 0x0601B4BD RID: 111805 RVA: 0x00375DE3 File Offset: 0x00373FE3
		// (set) Token: 0x0601B4BE RID: 111806 RVA: 0x00375DEB File Offset: 0x00373FEB
		public string Pattern { get; set; }

		// Token: 0x17009944 RID: 39236
		// (get) Token: 0x0601B4BF RID: 111807 RVA: 0x0000EE09 File Offset: 0x0000D009
		// (set) Token: 0x0601B4C0 RID: 111808 RVA: 0x0000EE09 File Offset: 0x0000D009
		public virtual int MaxLength
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17009945 RID: 39237
		// (get) Token: 0x0601B4C1 RID: 111809 RVA: 0x0000EE09 File Offset: 0x0000D009
		// (set) Token: 0x0601B4C2 RID: 111810 RVA: 0x0000EE09 File Offset: 0x0000D009
		public virtual int MinLength
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17009946 RID: 39238
		// (get) Token: 0x0601B4C3 RID: 111811 RVA: 0x0000EE09 File Offset: 0x0000D009
		// (set) Token: 0x0601B4C4 RID: 111812 RVA: 0x0000EE09 File Offset: 0x0000D009
		public virtual int Length
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0601B4C5 RID: 111813 RVA: 0x00375DF4 File Offset: 0x00373FF4
		public virtual string GetRestrictionValue(RestrictionField restrictionField)
		{
			switch (restrictionField)
			{
			case RestrictionField.Length:
				return this.Length.ToString(CultureInfo.CurrentUICulture);
			case RestrictionField.MinLength:
				return this.MinLength.ToString(CultureInfo.CurrentUICulture);
			case RestrictionField.Length | RestrictionField.MinLength:
				break;
			case RestrictionField.MaxLength:
				return this.MaxLength.ToString(CultureInfo.CurrentUICulture);
			default:
				if (restrictionField == RestrictionField.Pattern)
				{
					return this.Pattern;
				}
				break;
			}
			return string.Empty;
		}

		// Token: 0x17009947 RID: 39239
		// (get) Token: 0x0601B4C6 RID: 111814 RVA: 0x00375E6B File Offset: 0x0037406B
		// (set) Token: 0x0601B4C7 RID: 111815 RVA: 0x00375E73 File Offset: 0x00374073
		public RestrictionField RestrictionField { get; set; }

		// Token: 0x0601B4C8 RID: 111816 RVA: 0x00375E7C File Offset: 0x0037407C
		public virtual bool ValidateValueType(OpenXmlSimpleType attributeValue)
		{
			return attributeValue.HasValue;
		}

		// Token: 0x0601B4C9 RID: 111817 RVA: 0x00375E8C File Offset: 0x0037408C
		public RestrictionField Validate(OpenXmlSimpleType attributeValue)
		{
			RestrictionField restrictionField = RestrictionField.None;
			if ((byte)(this.RestrictionField & RestrictionField.Pattern) == 128 && !this.IsPatternValid(attributeValue))
			{
				restrictionField |= RestrictionField.Pattern;
			}
			if ((byte)(this.RestrictionField & RestrictionField.Length) == 1 && !this.IsLengthValid(attributeValue))
			{
				restrictionField |= RestrictionField.Length;
			}
			if ((byte)(this.RestrictionField & RestrictionField.MinLength) == 2 && !this.IsMinLengthValid(attributeValue))
			{
				restrictionField |= RestrictionField.MinLength;
			}
			if ((byte)(this.RestrictionField & RestrictionField.MaxLength) == 4 && !this.IsMaxLengthValid(attributeValue))
			{
				restrictionField |= RestrictionField.MaxLength;
			}
			if ((byte)(this.RestrictionField & RestrictionField.MinInclusive) == 8 && !this.IsMinInclusiveValid(attributeValue))
			{
				restrictionField |= RestrictionField.MinInclusive;
			}
			if ((byte)(this.RestrictionField & RestrictionField.MinExclusive) == 32 && !this.IsMinExclusiveValid(attributeValue))
			{
				restrictionField |= RestrictionField.MinExclusive;
			}
			if ((byte)(this.RestrictionField & RestrictionField.MaxInclusive) == 16 && !this.IsMaxInclusiveValid(attributeValue))
			{
				restrictionField |= RestrictionField.MaxInclusive;
			}
			if ((byte)(this.RestrictionField & RestrictionField.MaxExclusive) == 64 && !this.IsMaxExclusiveValid(attributeValue))
			{
				restrictionField |= RestrictionField.MaxExclusive;
			}
			return restrictionField;
		}

		// Token: 0x0601B4CA RID: 111818 RVA: 0x00002139 File Offset: 0x00000339
		public virtual bool IsPatternValid(OpenXmlSimpleType attributeValue)
		{
			return true;
		}

		// Token: 0x0601B4CB RID: 111819 RVA: 0x000091AE File Offset: 0x000073AE
		public virtual bool IsLengthValid(OpenXmlSimpleType attributeValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0601B4CC RID: 111820 RVA: 0x000091AE File Offset: 0x000073AE
		public virtual bool IsMinLengthValid(OpenXmlSimpleType attributeValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0601B4CD RID: 111821 RVA: 0x000091AE File Offset: 0x000073AE
		public virtual bool IsMaxLengthValid(OpenXmlSimpleType attributeValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0601B4CE RID: 111822 RVA: 0x000091AE File Offset: 0x000073AE
		public virtual bool IsMinInclusiveValid(OpenXmlSimpleType attributeValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0601B4CF RID: 111823 RVA: 0x000091AE File Offset: 0x000073AE
		public virtual bool IsMinExclusiveValid(OpenXmlSimpleType attributeValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0601B4D0 RID: 111824 RVA: 0x000091AE File Offset: 0x000073AE
		public virtual bool IsMaxInclusiveValid(OpenXmlSimpleType attributeValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0601B4D1 RID: 111825 RVA: 0x000091AE File Offset: 0x000073AE
		public virtual bool IsMaxExclusiveValid(OpenXmlSimpleType attributeValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400B520 RID: 46368
		[NonSerialized]
		private FileFormatVersions _fileFormat;
	}
}
