using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.RdlUpgrade;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel2010.Upgrade
{
	// Token: 0x0200006D RID: 109
	internal class SerializerHost2010 : SerializerHostBase
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x000167F4 File Offset: 0x000149F4
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x000167FC File Offset: 0x000149FC
		public List<IUpgradeable2010> Upgradeable2010
		{
			get
			{
				return this.m_upgradeable;
			}
			set
			{
				this.m_upgradeable = value;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00016808 File Offset: 0x00014A08
		public override Type GetSubstituteType(Type type)
		{
			if (!this.m_serializing)
			{
				for (int i = 0; i < SerializerHost2010.m_substituteTypes.GetLength(0); i++)
				{
					if (type == SerializerHost2010.m_substituteTypes[i, 0])
					{
						return SerializerHost2010.m_substituteTypes[i, 1];
					}
				}
			}
			else
			{
				for (int j = 0; j < SerializerHost2010.m_substituteTypes.GetLength(0); j++)
				{
					if (type == SerializerHost2010.m_substituteTypes[j, 1])
					{
						return SerializerHost2010.m_substituteTypes[j, 0];
					}
				}
			}
			return type;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001688E File Offset: 0x00014A8E
		public override void OnDeserialization(object value)
		{
			if (this.m_upgradeable != null && value is IUpgradeable2010)
			{
				this.m_upgradeable.Add((IUpgradeable2010)value);
			}
			base.OnDeserialization(value);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000168B8 File Offset: 0x00014AB8
		public override IEnumerable<ExtensionNamespace> GetExtensionNamespaces()
		{
			return new ExtensionNamespace[]
			{
				new ExtensionNamespace("rd", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner", false),
				new ExtensionNamespace("wa", "http://schemas.microsoft.com/sqlserver/reporting/webauthoring", false),
				new ExtensionNamespace("am", "http://schemas.microsoft.com/sqlserver/reporting/authoringmetadata", false),
				new ExtensionNamespace("ap", "http://schemas.microsoft.com/sqlserver/reporting/accessibilityproperties", false)
			};
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00016917 File Offset: 0x00014B17
		public SerializerHost2010(bool serializing)
			: base(serializing)
		{
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00016920 File Offset: 0x00014B20
		// Note: this type is marked as 'beforefieldinit'.
		static SerializerHost2010()
		{
			Type[,] array = new Type[2, 2];
			array[0, 0] = typeof(Report);
			array[0, 1] = typeof(Report2010);
			array[1, 0] = typeof(StateIndicator);
			array[1, 1] = typeof(StateIndicator2010);
			SerializerHost2010.m_substituteTypes = array;
		}

		// Token: 0x04000105 RID: 261
		private List<IUpgradeable2010> m_upgradeable;

		// Token: 0x04000106 RID: 262
		private static readonly Type[,] m_substituteTypes;
	}
}
