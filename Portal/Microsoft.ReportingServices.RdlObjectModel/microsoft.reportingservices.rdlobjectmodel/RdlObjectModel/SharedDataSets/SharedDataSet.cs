using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel.SharedDataSets
{
	// Token: 0x020002CF RID: 719
	[XmlElementClass("SharedDataSet", typeof(SharedDataSet), Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/shareddatasetdefinition")]
	public class SharedDataSet : ReportObject, IGlobalNamedObject, INamedObject
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x000330D1 File Offset: 0x000312D1
		// (set) Token: 0x06001618 RID: 5656 RVA: 0x000330E4 File Offset: 0x000312E4
		[XmlIgnore]
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

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x000330F3 File Offset: 0x000312F3
		// (set) Token: 0x0600161A RID: 5658 RVA: 0x00033106 File Offset: 0x00031306
		public string Description
		{
			get
			{
				return (string)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x00033115 File Offset: 0x00031315
		// (set) Token: 0x0600161C RID: 5660 RVA: 0x00033128 File Offset: 0x00031328
		public DataSet DataSet
		{
			get
			{
				return (DataSet)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x00033137 File Offset: 0x00031337
		// (set) Token: 0x0600161E RID: 5662 RVA: 0x0003313F File Offset: 0x0003133F
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		[DefaultValue("")]
		public string ReportServerUrl
		{
			get
			{
				return this.m_reportServerUrl;
			}
			set
			{
				this.m_reportServerUrl = value;
			}
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00033148 File Offset: 0x00031348
		public SharedDataSet()
		{
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00033150 File Offset: 0x00031350
		internal SharedDataSet(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0003315C File Offset: 0x0003135C
		private static bool ContainsMappingName(List<MemberMapping> list, string name)
		{
			using (List<MemberMapping>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Name.Equals(name))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x000331B8 File Offset: 0x000313B8
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x000331C0 File Offset: 0x000313C0
		public static void Serialize(Stream stream, SharedDataSet sharedDataSet)
		{
			if (sharedDataSet != null)
			{
				SharedDataSet.CreateSerializer().Serialize(stream, sharedDataSet);
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x000331D1 File Offset: 0x000313D1
		public static SharedDataSet Deserialize(Stream stream)
		{
			return (SharedDataSet)SharedDataSet.CreateSerializer().Deserialize(stream, typeof(SharedDataSet));
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000331F0 File Offset: 0x000313F0
		internal static RdlSerializer CreateSerializer()
		{
			SharedDataSet.SharedDataSetSerializerHost sharedDataSetSerializerHost = new SharedDataSet.SharedDataSetSerializerHost();
			return new RdlSerializer(new RdlSerializerSettings
			{
				Host = sharedDataSetSerializerHost
			});
		}

		// Token: 0x040006E3 RID: 1763
		private string m_reportServerUrl;

		// Token: 0x02000410 RID: 1040
		internal class SharedDataSetSerializerHost : ISerializerHost
		{
			// Token: 0x060018EC RID: 6380 RVA: 0x0003C06C File Offset: 0x0003A26C
			public Type GetSubstituteType(Type type)
			{
				return type;
			}

			// Token: 0x060018ED RID: 6381 RVA: 0x0003C06F File Offset: 0x0003A26F
			public void OnDeserialization(object value)
			{
			}

			// Token: 0x060018EE RID: 6382 RVA: 0x0003C071 File Offset: 0x0003A271
			public IEnumerable<ExtensionNamespace> GetExtensionNamespaces()
			{
				return new ExtensionNamespace[]
				{
					new ExtensionNamespace("rd", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner", false)
				};
			}
		}

		// Token: 0x02000411 RID: 1041
		internal class Definition : DefinitionStore<SharedDataSet, SharedDataSet.Definition.Properties>
		{
			// Token: 0x0200051D RID: 1309
			internal enum Properties
			{
				// Token: 0x04001148 RID: 4424
				Name,
				// Token: 0x04001149 RID: 4425
				Description,
				// Token: 0x0400114A RID: 4426
				DataSet
			}
		}
	}
}
