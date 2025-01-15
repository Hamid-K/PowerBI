using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003140 RID: 12608
	[Serializable]
	internal class StringRestriction : SimpleTypeRestriction
	{
		// Token: 0x17009991 RID: 39313
		// (get) Token: 0x0601B562 RID: 111970 RVA: 0x00376580 File Offset: 0x00374780
		// (set) Token: 0x0601B563 RID: 111971 RVA: 0x00376588 File Offset: 0x00374788
		public override int MaxLength { get; set; }

		// Token: 0x17009992 RID: 39314
		// (get) Token: 0x0601B564 RID: 111972 RVA: 0x00376591 File Offset: 0x00374791
		// (set) Token: 0x0601B565 RID: 111973 RVA: 0x00376599 File Offset: 0x00374799
		public override int MinLength { get; set; }

		// Token: 0x17009993 RID: 39315
		// (get) Token: 0x0601B566 RID: 111974 RVA: 0x003765A2 File Offset: 0x003747A2
		// (set) Token: 0x0601B567 RID: 111975 RVA: 0x003765AA File Offset: 0x003747AA
		public override int Length { get; set; }

		// Token: 0x17009994 RID: 39316
		// (get) Token: 0x0601B568 RID: 111976 RVA: 0x00002139 File Offset: 0x00000339
		// (set) Token: 0x0601B569 RID: 111977 RVA: 0x0000336E File Offset: 0x0000156E
		public override XsdType XsdType
		{
			get
			{
				return XsdType.String;
			}
			set
			{
			}
		}

		// Token: 0x17009995 RID: 39317
		// (get) Token: 0x0601B56A RID: 111978 RVA: 0x003765B3 File Offset: 0x003747B3
		public override string ClrTypeName
		{
			get
			{
				return StringRestriction.clrTypeName;
			}
		}

		// Token: 0x0601B56B RID: 111979 RVA: 0x003765BC File Offset: 0x003747BC
		public override bool IsPatternValid(OpenXmlSimpleType attributeValue)
		{
			if ((byte)(base.RestrictionField & RestrictionField.Pattern) == 128)
			{
				string text = "\\A(" + base.Pattern + ")\\z";
				return Regex.IsMatch(attributeValue.InnerText, text, RegexOptions.CultureInvariant);
			}
			return true;
		}

		// Token: 0x0601B56C RID: 111980 RVA: 0x00376606 File Offset: 0x00374806
		internal virtual int GetValueLength(OpenXmlSimpleType attributeValue)
		{
			return attributeValue.InnerText.Length;
		}

		// Token: 0x0601B56D RID: 111981 RVA: 0x00376613 File Offset: 0x00374813
		public override bool IsLengthValid(OpenXmlSimpleType attributeValue)
		{
			return (byte)(base.RestrictionField & RestrictionField.Length) != 1 || this.GetValueLength(attributeValue) == this.Length;
		}

		// Token: 0x0601B56E RID: 111982 RVA: 0x00376633 File Offset: 0x00374833
		public override bool IsMinLengthValid(OpenXmlSimpleType attributeValue)
		{
			return (byte)(base.RestrictionField & RestrictionField.MinLength) != 2 || this.GetValueLength(attributeValue) >= this.MinLength;
		}

		// Token: 0x0601B56F RID: 111983 RVA: 0x00376653 File Offset: 0x00374853
		public override bool IsMaxLengthValid(OpenXmlSimpleType attributeValue)
		{
			return (byte)(base.RestrictionField & RestrictionField.MaxLength) != 4 || this.GetValueLength(attributeValue) <= this.MaxLength;
		}

		// Token: 0x0400B532 RID: 46386
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[NonSerialized]
		private static readonly string clrTypeName = typeof(string).Name;
	}
}
