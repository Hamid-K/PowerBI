using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000CC RID: 204
	public class Textbox : ReportItem
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0001D70E File Offset: 0x0001B90E
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0001D716 File Offset: 0x0001B916
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		[DefaultValue(WatermarkText.None)]
		public WatermarkText WatermarkTextbox
		{
			get
			{
				return this.m_watermarkTextbox;
			}
			set
			{
				this.m_watermarkTextbox = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0001D71F File Offset: 0x0001B91F
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x0001D727 File Offset: 0x0001B927
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		[DefaultValue("")]
		public string DefaultName
		{
			get
			{
				return this.m_defaultName;
			}
			set
			{
				if (this.m_defaultName != value)
				{
					this.m_defaultName = value ?? string.Empty;
				}
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0001D747 File Offset: 0x0001B947
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x0001D756 File Offset: 0x0001B956
		[DefaultValue(false)]
		public bool CanGrow
		{
			get
			{
				return base.PropertyStore.GetBoolean(18);
			}
			set
			{
				base.PropertyStore.SetBoolean(18, value);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0001D766 File Offset: 0x0001B966
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0001D775 File Offset: 0x0001B975
		[DefaultValue(false)]
		public bool CanShrink
		{
			get
			{
				return base.PropertyStore.GetBoolean(19);
			}
			set
			{
				base.PropertyStore.SetBoolean(19, value);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0001D785 File Offset: 0x0001B985
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x0001D799 File Offset: 0x0001B999
		[DefaultValue("")]
		public string HideDuplicates
		{
			get
			{
				return (string)base.PropertyStore.GetObject(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0001D7A9 File Offset: 0x0001B9A9
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0001D7B8 File Offset: 0x0001B9B8
		[XmlElement(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/accessibilityproperties")]
		[DefaultValue(StructureTypeOverwriteType.None)]
		[ValidEnumValues("TextBoxStructureTypeOverwriteType")]
		public StructureTypeOverwriteType StructureTypeOverwrite
		{
			get
			{
				return (StructureTypeOverwriteType)base.PropertyStore.GetInteger(27);
			}
			set
			{
				base.PropertyStore.SetInteger(27, (int)value);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0001D7C8 File Offset: 0x0001B9C8
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0001D7DC File Offset: 0x0001B9DC
		public ToggleImage ToggleImage
		{
			get
			{
				return (ToggleImage)base.PropertyStore.GetObject(21);
			}
			set
			{
				base.PropertyStore.SetObject(21, value);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0001D7EC File Offset: 0x0001B9EC
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x0001D800 File Offset: 0x0001BA00
		public UserSort UserSort
		{
			get
			{
				return (UserSort)base.PropertyStore.GetObject(22);
			}
			set
			{
				base.PropertyStore.SetObject(22, value);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0001D810 File Offset: 0x0001BA10
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0001D81F File Offset: 0x0001BA1F
		[DefaultValue(DataElementStyles.Auto)]
		public DataElementStyles DataElementStyle
		{
			get
			{
				return (DataElementStyles)base.PropertyStore.GetInteger(23);
			}
			set
			{
				base.PropertyStore.SetInteger(23, (int)value);
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001D82F File Offset: 0x0001BA2F
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x0001D83E File Offset: 0x0001BA3E
		[DefaultValue(false)]
		public bool KeepTogether
		{
			get
			{
				return base.PropertyStore.GetBoolean(24);
			}
			set
			{
				base.PropertyStore.SetBoolean(24, value);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0001D84E File Offset: 0x0001BA4E
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x0001D862 File Offset: 0x0001BA62
		[XmlElement(typeof(RdlCollection<Paragraph>))]
		public IList<Paragraph> Paragraphs
		{
			get
			{
				return (IList<Paragraph>)base.PropertyStore.GetObject(25);
			}
			set
			{
				base.PropertyStore.SetObject(25, value);
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001D872 File Offset: 0x0001BA72
		public Textbox()
		{
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001D885 File Offset: 0x0001BA85
		internal Textbox(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001D899 File Offset: 0x0001BA99
		public override void Initialize()
		{
			base.Initialize();
			this.Paragraphs = new RdlCollection<Paragraph>();
			this.Paragraphs.Add(new Paragraph());
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001D8BC File Offset: 0x0001BABC
		internal override void UpdateNamedReferences(NameChanges nameChanges)
		{
			base.UpdateNamedReferences(nameChanges);
			string hideDuplicates = this.HideDuplicates;
			this.HideDuplicates = nameChanges.GetNewName(NameChanges.EntryType.DataSet, this.HideDuplicates);
			if (hideDuplicates == this.HideDuplicates)
			{
				this.HideDuplicates = nameChanges.GetNewName(NameChanges.EntryType.Group, this.HideDuplicates);
			}
		}

		// Token: 0x04000182 RID: 386
		private string m_defaultName = string.Empty;

		// Token: 0x04000183 RID: 387
		private WatermarkText m_watermarkTextbox;

		// Token: 0x02000375 RID: 885
		internal new class Definition : DefinitionStore<Textbox, Textbox.Definition.Properties>
		{
			// Token: 0x0600180A RID: 6154 RVA: 0x0003B26C File Offset: 0x0003946C
			private Definition()
			{
			}

			// Token: 0x02000490 RID: 1168
			internal enum Properties
			{
				// Token: 0x04000B97 RID: 2967
				Style,
				// Token: 0x04000B98 RID: 2968
				Name,
				// Token: 0x04000B99 RID: 2969
				ActionInfo,
				// Token: 0x04000B9A RID: 2970
				Top,
				// Token: 0x04000B9B RID: 2971
				Left,
				// Token: 0x04000B9C RID: 2972
				Height,
				// Token: 0x04000B9D RID: 2973
				Width,
				// Token: 0x04000B9E RID: 2974
				ZIndex,
				// Token: 0x04000B9F RID: 2975
				Visibility,
				// Token: 0x04000BA0 RID: 2976
				ToolTip,
				// Token: 0x04000BA1 RID: 2977
				ToolTipLocID,
				// Token: 0x04000BA2 RID: 2978
				DocumentMapLabel,
				// Token: 0x04000BA3 RID: 2979
				DocumentMapLabelLocID,
				// Token: 0x04000BA4 RID: 2980
				Bookmark,
				// Token: 0x04000BA5 RID: 2981
				RepeatWith,
				// Token: 0x04000BA6 RID: 2982
				CustomProperties,
				// Token: 0x04000BA7 RID: 2983
				DataElementName,
				// Token: 0x04000BA8 RID: 2984
				DataElementOutput,
				// Token: 0x04000BA9 RID: 2985
				CanGrow,
				// Token: 0x04000BAA RID: 2986
				CanShrink,
				// Token: 0x04000BAB RID: 2987
				HideDuplicates,
				// Token: 0x04000BAC RID: 2988
				ToggleImage,
				// Token: 0x04000BAD RID: 2989
				UserSort,
				// Token: 0x04000BAE RID: 2990
				DataElementStyle,
				// Token: 0x04000BAF RID: 2991
				KeepTogether,
				// Token: 0x04000BB0 RID: 2992
				Paragraphs,
				// Token: 0x04000BB1 RID: 2993
				PropertyCount,
				// Token: 0x04000BB2 RID: 2994
				StructureTypeOverwrite
			}
		}
	}
}
