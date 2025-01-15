using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000531 RID: 1329
	internal struct DataReaderRIFObjectCreator : IRIFObjectCreator
	{
		// Token: 0x06004866 RID: 18534 RVA: 0x001320C4 File Offset: 0x001302C4
		public IPersistable CreateRIFObject(ObjectType objectType, ref IntermediateFormatReader context)
		{
			IPersistable persistable = null;
			if (objectType == ObjectType.Null)
			{
				return null;
			}
			switch (objectType)
			{
			case ObjectType.RecordSetInfo:
				persistable = new RecordSetInfo();
				break;
			case ObjectType.RecordRow:
				persistable = new RecordRow();
				break;
			case ObjectType.RecordField:
				persistable = new RecordField();
				break;
			case ObjectType.RecordSetPropertyNames:
				persistable = new RecordSetPropertyNames();
				break;
			default:
				if (objectType != ObjectType.IntermediateFormatVersion)
				{
					Global.Tracer.Assert(false, string.Format("DataReaderRIFObjectCreator does not support {0}.{1}.", objectType.GetType(), objectType));
				}
				else
				{
					persistable = new IntermediateFormatVersion();
				}
				break;
			}
			persistable.Deserialize(context);
			return persistable;
		}
	}
}
