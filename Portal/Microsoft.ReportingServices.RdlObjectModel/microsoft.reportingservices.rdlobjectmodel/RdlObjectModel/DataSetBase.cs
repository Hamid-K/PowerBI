using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000BB RID: 187
	public abstract class DataSetBase : ReportObject, IGlobalNamedObject, INamedObject
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0001BB79 File Offset: 0x00019D79
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0001BB8C File Offset: 0x00019D8C
		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001BB9B File Offset: 0x00019D9B
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x0001BBA9 File Offset: 0x00019DA9
		[DefaultValue(CaseSensitivities.Auto)]
		public CaseSensitivities CaseSensitivity
		{
			get
			{
				return (CaseSensitivities)base.PropertyStore.GetInteger(1);
			}
			set
			{
				base.PropertyStore.SetInteger(1, (int)value);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0001BBB8 File Offset: 0x00019DB8
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x0001BBCB File Offset: 0x00019DCB
		[DefaultValue("")]
		public string Collation
		{
			get
			{
				return (string)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0001BBDA File Offset: 0x00019DDA
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0001BBE8 File Offset: 0x00019DE8
		[DefaultValue(AccentSensitivities.Auto)]
		public AccentSensitivities AccentSensitivity
		{
			get
			{
				return (AccentSensitivities)base.PropertyStore.GetInteger(3);
			}
			set
			{
				base.PropertyStore.SetInteger(3, (int)value);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0001BBF7 File Offset: 0x00019DF7
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0001BC05 File Offset: 0x00019E05
		[DefaultValue(KanatypeSensitivities.Auto)]
		public KanatypeSensitivities KanatypeSensitivity
		{
			get
			{
				return (KanatypeSensitivities)base.PropertyStore.GetInteger(4);
			}
			set
			{
				base.PropertyStore.SetInteger(4, (int)value);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0001BC14 File Offset: 0x00019E14
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x0001BC22 File Offset: 0x00019E22
		[DefaultValue(WidthSensitivities.Auto)]
		public WidthSensitivities WidthSensitivity
		{
			get
			{
				return (WidthSensitivities)base.PropertyStore.GetInteger(5);
			}
			set
			{
				base.PropertyStore.SetInteger(5, (int)value);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0001BC31 File Offset: 0x00019E31
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x0001BC3F File Offset: 0x00019E3F
		[DefaultValue(InterpretSubtotalsAsDetailsTypes.Auto)]
		public InterpretSubtotalsAsDetailsTypes InterpretSubtotalsAsDetails
		{
			get
			{
				return (InterpretSubtotalsAsDetailsTypes)base.PropertyStore.GetInteger(6);
			}
			set
			{
				base.PropertyStore.SetInteger(6, (int)value);
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001BC4E File Offset: 0x00019E4E
		public DataSetBase()
		{
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001BC56 File Offset: 0x00019E56
		internal DataSetBase(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060007D2 RID: 2002
		public abstract QueryBase GetQuery();

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001BC5F File Offset: 0x00019E5F
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x02000367 RID: 871
		internal class Definition : DefinitionStore<DataSetBase, DataSetBase.Definition.Properties>
		{
			// Token: 0x02000484 RID: 1156
			internal enum Properties
			{
				// Token: 0x04000B06 RID: 2822
				Name,
				// Token: 0x04000B07 RID: 2823
				CaseSensitivity,
				// Token: 0x04000B08 RID: 2824
				Collation,
				// Token: 0x04000B09 RID: 2825
				AccentSensitivity,
				// Token: 0x04000B0A RID: 2826
				KanatypeSensitivity,
				// Token: 0x04000B0B RID: 2827
				WidthSensitivity,
				// Token: 0x04000B0C RID: 2828
				InterpretSubtotalsAsDetails
			}
		}
	}
}
