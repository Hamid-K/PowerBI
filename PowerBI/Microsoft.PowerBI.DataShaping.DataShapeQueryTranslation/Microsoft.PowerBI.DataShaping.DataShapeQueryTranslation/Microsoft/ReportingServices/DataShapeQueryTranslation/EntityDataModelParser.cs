using System;
using System.IO;
using System.Xml;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.Errors;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000057 RID: 87
	internal sealed class EntityDataModelParser : IModelParser
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x0000D5B9 File Offset: 0x0000B7B9
		private EntityDataModelParser()
		{
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		public object Parse(Stream model)
		{
			object obj;
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(model, XmlUtils.ApplyDtdDosDefense(new XmlReaderSettings())))
				{
					obj = EntityDataModel.Load(xmlReader);
				}
			}
			catch (Exception ex)
			{
				if (ErrorUtils.IsStoppingException(ex))
				{
					throw;
				}
				throw new DataModelParsingException("DataModelParsingError", ex.Message, ex);
			}
			return obj;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000D630 File Offset: 0x0000B830
		public EntityDataModel Parse(string model)
		{
			EntityDataModel entityDataModel;
			try
			{
				using (StringReader stringReader = new StringReader(model))
				{
					using (XmlReader xmlReader = XmlReader.Create(stringReader, XmlUtils.ApplyDtdDosDefense(new XmlReaderSettings())))
					{
						entityDataModel = EntityDataModel.Load(xmlReader);
					}
				}
			}
			catch (Exception ex)
			{
				if (ErrorUtils.IsStoppingException(ex))
				{
					throw;
				}
				throw new DataModelParsingException("DataModelParsingError", ex.Message, ex);
			}
			return entityDataModel;
		}

		// Token: 0x040001B2 RID: 434
		internal static readonly EntityDataModelParser Instance = new EntityDataModelParser();
	}
}
