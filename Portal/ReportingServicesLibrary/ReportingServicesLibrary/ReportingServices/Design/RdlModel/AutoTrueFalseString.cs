using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003CA RID: 970
	[TypeConverter(typeof(AutoTrueFalseString.AutoTrueFalseStringConverter))]
	public sealed class AutoTrueFalseString : XmlString
	{
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x0007E2F3 File Offset: 0x0007C4F3
		protected internal override Hashtable ValuesHash
		{
			get
			{
				return AutoTrueFalseString.m_valuesHash;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x0007E2FA File Offset: 0x0007C4FA
		protected internal override string DefaultValue
		{
			get
			{
				return "Auto";
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0007E301 File Offset: 0x0007C501
		protected internal override string[] SortedDisplayStrings
		{
			get
			{
				return Constants.AutoTrueFalseTypes;
			}
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x0007E308 File Offset: 0x0007C508
		public AutoTrueFalseString()
		{
			base.RawValue = "Auto";
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x0007E31B File Offset: 0x0007C51B
		internal AutoTrueFalseString(string value)
		{
			base.RawValue = value;
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0007E32C File Offset: 0x0007C52C
		private static Hashtable GetStandardValues()
		{
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < Constants.AutoTrueFalseTypes.Length; i++)
			{
				hashtable.Add(Constants.AutoTrueFalseTypes[i], Constants.AutoTrueFalseStrings[i]);
			}
			return hashtable;
		}

		// Token: 0x04000D9C RID: 3484
		private static Hashtable m_valuesHash = AutoTrueFalseString.GetStandardValues();

		// Token: 0x02000517 RID: 1303
		internal sealed class AutoTrueFalseStringConverter : XmlString.XmlStringListConverter
		{
			// Token: 0x06002514 RID: 9492 RVA: 0x00087874 File Offset: 0x00085A74
			protected override XmlString CreateObject(string value)
			{
				return new AutoTrueFalseString(value);
			}
		}
	}
}
