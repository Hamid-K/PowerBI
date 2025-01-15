using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2010.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2008.Upgrade
{
	// Token: 0x02000074 RID: 116
	internal class SerializerHost2008 : SerializerHost2010
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00016D20 File Offset: 0x00014F20
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x00016D28 File Offset: 0x00014F28
		public List<IUpgradeable2008> Upgradeable2008
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

		// Token: 0x0600042A RID: 1066 RVA: 0x00016D34 File Offset: 0x00014F34
		public override Type GetSubstituteType(Type type)
		{
			if (!this.m_serializing)
			{
				for (int i = 0; i < SerializerHost2008.m_substituteTypes.GetLength(0); i++)
				{
					if (type == SerializerHost2008.m_substituteTypes[i, 0])
					{
						return SerializerHost2008.m_substituteTypes[i, 1];
					}
				}
			}
			else
			{
				for (int j = 0; j < SerializerHost2008.m_substituteTypes.GetLength(0); j++)
				{
					if (type == SerializerHost2008.m_substituteTypes[j, 1])
					{
						return SerializerHost2008.m_substituteTypes[j, 0];
					}
				}
			}
			return type;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00016DBA File Offset: 0x00014FBA
		public override void OnDeserialization(object value)
		{
			if (this.m_upgradeable != null && value is IUpgradeable2008)
			{
				this.m_upgradeable.Add((IUpgradeable2008)value);
			}
			base.OnDeserialization(value);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00016DE4 File Offset: 0x00014FE4
		public override IEnumerable<ExtensionNamespace> GetExtensionNamespaces()
		{
			return new ExtensionNamespace[]
			{
				new ExtensionNamespace("rd", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner", false)
			};
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00016DFF File Offset: 0x00014FFF
		public SerializerHost2008(bool serializing)
			: base(serializing)
		{
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00016E08 File Offset: 0x00015008
		// Note: this type is marked as 'beforefieldinit'.
		static SerializerHost2008()
		{
			Type[,] array = new Type[1, 2];
			array[0, 0] = typeof(Report);
			array[0, 1] = typeof(Report2008);
			SerializerHost2008.m_substituteTypes = array;
		}

		// Token: 0x0400010D RID: 269
		private List<IUpgradeable2008> m_upgradeable;

		// Token: 0x0400010E RID: 270
		private static readonly Type[,] m_substituteTypes;
	}
}
