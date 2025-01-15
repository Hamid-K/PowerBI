using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200042A RID: 1066
	[TypeConverter(typeof(ToggleImageState.ToggleImageStateConverter))]
	public class ToggleImageState : XmlString
	{
		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x00081B67 File Offset: 0x0007FD67
		protected internal override Hashtable ValuesHash
		{
			get
			{
				return ToggleImageState.m_valuesHash;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x00081B6E File Offset: 0x0007FD6E
		protected internal override ICollection StandardValues
		{
			get
			{
				return ToggleImageState.m_xmlStrings;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x00081B75 File Offset: 0x0007FD75
		protected internal override string DefaultValue
		{
			get
			{
				return "false";
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x000053DC File Offset: 0x000035DC
		protected internal override bool AllowExpressions
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x00081B7C File Offset: 0x0007FD7C
		protected internal override string[] SortedDisplayStrings
		{
			get
			{
				return ToggleImageState.m_displayStrings;
			}
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000802E3 File Offset: 0x0007E4E3
		public ToggleImageState()
		{
			base.RawValue = this.DefaultValue;
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0007E31B File Offset: 0x0007C51B
		public ToggleImageState(string value)
		{
			base.RawValue = value;
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x00081B84 File Offset: 0x0007FD84
		private static Hashtable GetValuesHash()
		{
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < ToggleImageState.m_xmlStrings.Length; i++)
			{
				hashtable.Add(ToggleImageState.m_xmlStrings[i], ToggleImageState.m_displayStrings[i]);
			}
			return hashtable;
		}

		// Token: 0x04000EE8 RID: 3816
		private static string[] m_displayStrings = new string[] { "Collapsed", "Expanded" };

		// Token: 0x04000EE9 RID: 3817
		private static string[] m_xmlStrings = new string[] { "false", "true" };

		// Token: 0x04000EEA RID: 3818
		private static Hashtable m_valuesHash = ToggleImageState.GetValuesHash();

		// Token: 0x02000529 RID: 1321
		internal sealed class ToggleImageStateConverter : XmlString.XmlStringListConverter
		{
			// Token: 0x0600252B RID: 9515 RVA: 0x00087C05 File Offset: 0x00085E05
			protected override XmlString CreateObject(string value)
			{
				return new ToggleImageState(value);
			}
		}
	}
}
