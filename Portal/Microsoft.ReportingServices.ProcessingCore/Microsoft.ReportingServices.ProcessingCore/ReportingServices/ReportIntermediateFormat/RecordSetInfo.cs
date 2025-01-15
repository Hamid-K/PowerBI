using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004F4 RID: 1268
	[Serializable]
	public sealed class RecordSetInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06004077 RID: 16503 RVA: 0x00110083 File Offset: 0x0010E283
		internal RecordSetInfo()
		{
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x00110098 File Offset: 0x0010E298
		internal RecordSetInfo(bool readerExtensionsSupported, bool persistCalculatedFields, DataSetInstance dataSetInstance, DateTime reportExecutionTime)
		{
			this.m_readerExtensionsSupported = readerExtensionsSupported;
			this.m_compareOptions = dataSetInstance.DataSetDef.GetCLRCompareOptions();
			this.m_commandText = dataSetInstance.CommandText;
			this.m_rewrittenCommandText = dataSetInstance.RewrittenCommandText;
			this.m_cultureName = dataSetInstance.DataSetDef.CreateCultureInfoFromLcid().Name;
			this.m_executionTime = dataSetInstance.GetQueryExecutionTime(reportExecutionTime);
			int count = dataSetInstance.DataSetDef.Fields.Count;
			if (count > 0)
			{
				int num = 0;
				if (persistCalculatedFields)
				{
					this.m_fieldNames = new string[count];
				}
				else
				{
					this.m_fieldNames = new string[dataSetInstance.DataSetDef.NonCalculatedFieldCount];
				}
				for (int i = 0; i < count; i++)
				{
					if (persistCalculatedFields || !dataSetInstance.DataSetDef.Fields[i].IsCalculatedField)
					{
						this.m_fieldNames[num++] = dataSetInstance.DataSetDef.Fields[i].Name;
					}
				}
			}
		}

		// Token: 0x17001B2B RID: 6955
		// (get) Token: 0x06004079 RID: 16505 RVA: 0x00110191 File Offset: 0x0010E391
		internal bool ReaderExtensionsSupported
		{
			get
			{
				return this.m_readerExtensionsSupported;
			}
		}

		// Token: 0x17001B2C RID: 6956
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x00110199 File Offset: 0x0010E399
		internal RecordSetPropertyNamesList FieldPropertyNames
		{
			get
			{
				return this.m_fieldPropertyNames;
			}
		}

		// Token: 0x17001B2D RID: 6957
		// (get) Token: 0x0600407B RID: 16507 RVA: 0x001101A1 File Offset: 0x0010E3A1
		internal CompareOptions CompareOptions
		{
			get
			{
				return this.m_compareOptions;
			}
		}

		// Token: 0x17001B2E RID: 6958
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x001101A9 File Offset: 0x0010E3A9
		internal string[] FieldNames
		{
			get
			{
				return this.m_fieldNames;
			}
		}

		// Token: 0x17001B2F RID: 6959
		// (get) Token: 0x0600407D RID: 16509 RVA: 0x001101B1 File Offset: 0x0010E3B1
		internal string CommandText
		{
			get
			{
				return this.m_commandText;
			}
		}

		// Token: 0x17001B30 RID: 6960
		// (get) Token: 0x0600407E RID: 16510 RVA: 0x001101B9 File Offset: 0x0010E3B9
		internal string RewrittenCommandText
		{
			get
			{
				return this.m_rewrittenCommandText;
			}
		}

		// Token: 0x17001B31 RID: 6961
		// (get) Token: 0x0600407F RID: 16511 RVA: 0x001101C1 File Offset: 0x0010E3C1
		internal string CultureName
		{
			get
			{
				return this.m_cultureName;
			}
		}

		// Token: 0x17001B32 RID: 6962
		// (get) Token: 0x06004080 RID: 16512 RVA: 0x001101C9 File Offset: 0x0010E3C9
		internal DateTime ExecutionTime
		{
			get
			{
				return this.m_executionTime;
			}
		}

		// Token: 0x17001B33 RID: 6963
		// (get) Token: 0x06004081 RID: 16513 RVA: 0x001101D1 File Offset: 0x0010E3D1
		internal bool ValidCompareOptions
		{
			get
			{
				return this.m_validCompareOptions;
			}
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x001101DC File Offset: 0x0010E3DC
		internal void PopulateExtendedFieldsProperties(DataSetInstance dataSetInstance)
		{
			if (dataSetInstance.FieldInfos != null)
			{
				int num = dataSetInstance.FieldInfos.Length;
				this.m_fieldPropertyNames = new RecordSetPropertyNamesList(num);
				for (int i = 0; i < num; i++)
				{
					FieldInfo fieldInfo = dataSetInstance.FieldInfos[i];
					RecordSetPropertyNames recordSetPropertyNames = null;
					if (fieldInfo != null && fieldInfo.PropertyCount != 0)
					{
						recordSetPropertyNames = new RecordSetPropertyNames();
						recordSetPropertyNames.PropertyNames = new List<string>(fieldInfo.PropertyCount);
						recordSetPropertyNames.PropertyNames.AddRange(fieldInfo.PropertyNames);
					}
					this.m_fieldPropertyNames.Add(recordSetPropertyNames);
				}
			}
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x00110260 File Offset: 0x0010E460
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RecordSetInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ReaderExtensionsSupported, Token.Boolean),
				new MemberInfo(MemberName.FieldPropertyNames, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RecordSetPropertyNames),
				new MemberInfo(MemberName.CompareOptions, Token.Enum),
				new MemberInfo(MemberName.FieldNames, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Token.String),
				new MemberInfo(MemberName.CommandText, Token.String),
				new MemberInfo(MemberName.RewrittenCommandText, Token.String),
				new MemberInfo(MemberName.CultureName, Token.String),
				new MemberInfo(MemberName.ExecutionTime, Token.DateTime)
			});
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x00110310 File Offset: 0x0010E510
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RecordSetInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.CommandText)
				{
					switch (memberName)
					{
					case MemberName.ReaderExtensionsSupported:
						writer.Write(this.m_readerExtensionsSupported);
						continue;
					case MemberName.FieldPropertyNames:
						writer.Write(this.m_fieldPropertyNames);
						continue;
					case MemberName.CompareOptions:
						writer.WriteEnum((int)this.m_compareOptions);
						continue;
					default:
						if (memberName == MemberName.RewrittenCommandText)
						{
							writer.Write(this.m_rewrittenCommandText);
							continue;
						}
						if (memberName == MemberName.CommandText)
						{
							writer.Write(this.m_commandText);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.ExecutionTime)
					{
						writer.Write(this.m_executionTime);
						continue;
					}
					if (memberName == MemberName.CultureName)
					{
						writer.Write(this.m_cultureName);
						continue;
					}
					if (memberName == MemberName.FieldNames)
					{
						writer.Write(this.m_fieldNames);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x00110414 File Offset: 0x0010E614
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RecordSetInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.CommandText)
				{
					switch (memberName)
					{
					case MemberName.ReaderExtensionsSupported:
						this.m_readerExtensionsSupported = reader.ReadBoolean();
						continue;
					case MemberName.FieldPropertyNames:
						this.m_fieldPropertyNames = reader.ReadListOfRIFObjects<RecordSetPropertyNamesList>();
						continue;
					case MemberName.CompareOptions:
						this.m_compareOptions = (CompareOptions)reader.ReadEnum();
						continue;
					default:
						if (memberName == MemberName.RewrittenCommandText)
						{
							this.m_rewrittenCommandText = reader.ReadString();
							continue;
						}
						if (memberName == MemberName.CommandText)
						{
							this.m_commandText = reader.ReadString();
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.ExecutionTime)
					{
						this.m_executionTime = reader.ReadDateTime();
						continue;
					}
					if (memberName == MemberName.CultureName)
					{
						this.m_cultureName = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.FieldNames)
					{
						this.m_fieldNames = reader.ReadStringArray();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x00110516 File Offset: 0x0010E716
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x00110523 File Offset: 0x0010E723
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RecordSetInfo;
		}

		// Token: 0x04001D97 RID: 7575
		private bool m_readerExtensionsSupported;

		// Token: 0x04001D98 RID: 7576
		private RecordSetPropertyNamesList m_fieldPropertyNames;

		// Token: 0x04001D99 RID: 7577
		private string[] m_fieldNames;

		// Token: 0x04001D9A RID: 7578
		private CompareOptions m_compareOptions;

		// Token: 0x04001D9B RID: 7579
		private string m_commandText;

		// Token: 0x04001D9C RID: 7580
		private string m_rewrittenCommandText;

		// Token: 0x04001D9D RID: 7581
		private string m_cultureName;

		// Token: 0x04001D9E RID: 7582
		private DateTime m_executionTime = DateTime.MinValue;

		// Token: 0x04001D9F RID: 7583
		[NonSerialized]
		private bool m_validCompareOptions;

		// Token: 0x04001DA0 RID: 7584
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = RecordSetInfo.GetDeclaration();
	}
}
