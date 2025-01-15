using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000532 RID: 1330
	internal struct GroupTreeRIFObjectCreator : IRIFObjectCreator, IScalabilityObjectCreator
	{
		// Token: 0x06004867 RID: 18535 RVA: 0x00132158 File Offset: 0x00130358
		public IPersistable CreateRIFObject(ObjectType objectType, ref IntermediateFormatReader context)
		{
			IPersistable persistable = null;
			if (objectType == ObjectType.Null)
			{
				return null;
			}
			Global.Tracer.Assert(this.TryCreateObject(objectType, out persistable));
			persistable.Deserialize(context);
			return persistable;
		}

		// Token: 0x06004868 RID: 18536 RVA: 0x0013218C File Offset: 0x0013038C
		public bool TryCreateObject(ObjectType objectType, out IPersistable persistObj)
		{
			if (objectType <= ObjectType.SubReportInfo)
			{
				if (objectType <= ObjectType.ValidValue)
				{
					if (objectType == ObjectType.DataCellInstanceList)
					{
						persistObj = new DataCellInstanceList();
						return true;
					}
					switch (objectType)
					{
					case ObjectType.ReportSnapshot:
						persistObj = new ReportSnapshot();
						return true;
					case ObjectType.DocumentMapNode:
					case ObjectType.InstanceInfo:
					case ObjectType.ScopeInstance:
						break;
					case ObjectType.ReportInstance:
						persistObj = new ReportInstance();
						return true;
					case ObjectType.ParameterInfo:
						persistObj = new ParameterInfo();
						return true;
					case ObjectType.ParameterInfoCollection:
						persistObj = new ParameterInfoCollection();
						return true;
					default:
						if (objectType == ObjectType.ValidValue)
						{
							persistObj = new ValidValue();
							return true;
						}
						break;
					}
				}
				else
				{
					if (objectType == ObjectType.IntermediateFormatVersion)
					{
						persistObj = new IntermediateFormatVersion();
						return true;
					}
					if (objectType == ObjectType.ImageInfo)
					{
						persistObj = new ImageInfo();
						return true;
					}
					switch (objectType)
					{
					case ObjectType.SubReportInstance:
						persistObj = new SubReportInstance();
						return true;
					case ObjectType.Parameter:
						persistObj = new ParameterImplWrapper();
						return true;
					case ObjectType.OnDemandMetadata:
						persistObj = new OnDemandMetadata();
						return true;
					case ObjectType.GroupTreePartition:
						persistObj = new GroupTreePartition();
						return true;
					case ObjectType.FieldInfo:
						persistObj = new FieldInfo();
						return true;
					case ObjectType.DataSetInstance:
						persistObj = new DataSetInstance();
						return true;
					case ObjectType.DataRegionInstance:
						persistObj = new DataRegionInstance();
						return true;
					case ObjectType.DataRegionMemberInstance:
						persistObj = new DataRegionMemberInstance();
						return true;
					case ObjectType.DataCellInstance:
						persistObj = new DataCellInstance();
						return true;
					case ObjectType.DataAggregateObjResult:
						persistObj = new DataAggregateObjResult();
						return true;
					case ObjectType.Parameters:
						persistObj = new ParametersImplWrapper();
						return true;
					case ObjectType.SubReportInfo:
						persistObj = new SubReportInfo();
						return true;
					}
				}
			}
			else if (objectType <= ObjectType.TreePartitionManager)
			{
				if (objectType == ObjectType.CommonSubReportInfo)
				{
					persistObj = new CommonSubReportInfo();
					return true;
				}
				if (objectType == ObjectType.LookupObjResult)
				{
					persistObj = new LookupObjResult();
					return true;
				}
				if (objectType == ObjectType.TreePartitionManager)
				{
					persistObj = new TreePartitionManager();
					return true;
				}
			}
			else if (objectType <= ObjectType.UpdatedVariableValues)
			{
				if (objectType == ObjectType.ShapefileInfo)
				{
					persistObj = new ShapefileInfo();
					return true;
				}
				if (objectType == ObjectType.UpdatedVariableValues)
				{
					persistObj = new UpdatedVariableValues();
					return true;
				}
			}
			else
			{
				if (objectType == ObjectType.ParametersLayout)
				{
					persistObj = new ParametersGridLayout();
					return true;
				}
				if (objectType == ObjectType.ParameterGridLayoutCellDefinition)
				{
					persistObj = new ParameterGridLayoutCellDefinition();
					return true;
				}
			}
			persistObj = null;
			return false;
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x001323EB File Offset: 0x001305EB
		public List<Declaration> GetDeclarations()
		{
			return ChunkManager.OnDemandProcessingManager.GetChunkDeclarations();
		}
	}
}
