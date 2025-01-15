using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000410 RID: 1040
	[TypeConverter(typeof(TrueFalseString.TrueFalseStringConverter))]
	public sealed class TrueFalseString : XmlString
	{
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x000802C7 File Offset: 0x0007E4C7
		protected internal override Hashtable ValuesHash
		{
			get
			{
				return TrueFalseString.m_valuesHash;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x000802CE File Offset: 0x0007E4CE
		protected internal override ICollection StandardValues
		{
			get
			{
				return TrueFalseString.m_xmlStrings;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x000802D5 File Offset: 0x0007E4D5
		protected internal override string DefaultValue
		{
			get
			{
				return "false";
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x000053DC File Offset: 0x000035DC
		protected internal override bool AllowExpressions
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x000802DC File Offset: 0x0007E4DC
		protected internal override string[] SortedDisplayStrings
		{
			get
			{
				return TrueFalseString.m_displayStrings;
			}
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000802E3 File Offset: 0x0007E4E3
		public TrueFalseString()
		{
			base.RawValue = this.DefaultValue;
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x0007E31B File Offset: 0x0007C51B
		public TrueFalseString(string value)
		{
			base.RawValue = value;
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000802F8 File Offset: 0x0007E4F8
		private static Hashtable GetValuesHash()
		{
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < TrueFalseString.m_xmlStrings.Length; i++)
			{
				hashtable.Add(TrueFalseString.m_xmlStrings[i], TrueFalseString.m_displayStrings[i]);
			}
			return hashtable;
		}

		// Token: 0x04000E7B RID: 3707
		private static string[] m_displayStrings = new string[] { "True", "False" };

		// Token: 0x04000E7C RID: 3708
		private static string[] m_xmlStrings = new string[] { "true", "false" };

		// Token: 0x04000E7D RID: 3709
		private static Hashtable m_valuesHash = TrueFalseString.GetValuesHash();

		// Token: 0x02000526 RID: 1318
		internal sealed class TrueFalseStringConverter : XmlString.XmlStringListConverter
		{
			// Token: 0x06002524 RID: 9508 RVA: 0x00087BA9 File Offset: 0x00085DA9
			protected override XmlString CreateObject(string value)
			{
				return new TrueFalseString(value);
			}
		}
	}
}
