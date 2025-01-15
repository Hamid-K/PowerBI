using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.SharedDataSets
{
	// Token: 0x020002CE RID: 718
	public class DataSetParameter : ReportObject, INamedObject
	{
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00032FEE File Offset: 0x000311EE
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x00032FF6 File Offset: 0x000311F6
		[DefaultValue(false)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public bool UserDefined
		{
			get
			{
				return this.m_userDefined;
			}
			set
			{
				if (this.m_userDefined != value)
				{
					this.m_userDefined = value;
				}
			}
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00033008 File Offset: 0x00031208
		public override void Initialize()
		{
			base.Initialize();
			this.ReadOnly = false;
			this.Nullable = false;
			this.OmitFromQuery = false;
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00033025 File Offset: 0x00031225
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x00033038 File Offset: 0x00031238
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

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00033047 File Offset: 0x00031247
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x00033055 File Offset: 0x00031255
		public ReportExpression? DefaultValue
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression?>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00033069 File Offset: 0x00031269
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x00033077 File Offset: 0x00031277
		public bool ReadOnly
		{
			get
			{
				return base.PropertyStore.GetBoolean(2);
			}
			set
			{
				base.PropertyStore.SetBoolean(2, value);
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x00033086 File Offset: 0x00031286
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x00033094 File Offset: 0x00031294
		public bool Nullable
		{
			get
			{
				return base.PropertyStore.GetBoolean(3);
			}
			set
			{
				base.PropertyStore.SetBoolean(3, value);
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x000330A3 File Offset: 0x000312A3
		// (set) Token: 0x06001614 RID: 5652 RVA: 0x000330B1 File Offset: 0x000312B1
		public bool OmitFromQuery
		{
			get
			{
				return base.PropertyStore.GetBoolean(4);
			}
			set
			{
				base.PropertyStore.SetBoolean(4, value);
			}
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x000330C0 File Offset: 0x000312C0
		public DataSetParameter()
		{
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x000330C8 File Offset: 0x000312C8
		internal DataSetParameter(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x040006E2 RID: 1762
		private bool m_userDefined;

		// Token: 0x0200040F RID: 1039
		internal class Definition : DefinitionStore<DataSetParameter, DataSetParameter.Definition.Properties>
		{
			// Token: 0x0200051C RID: 1308
			internal enum Properties
			{
				// Token: 0x04001142 RID: 4418
				Name,
				// Token: 0x04001143 RID: 4419
				DefaultValue,
				// Token: 0x04001144 RID: 4420
				ReadOnly,
				// Token: 0x04001145 RID: 4421
				Nullable,
				// Token: 0x04001146 RID: 4422
				OmitFromQuery
			}
		}
	}
}
