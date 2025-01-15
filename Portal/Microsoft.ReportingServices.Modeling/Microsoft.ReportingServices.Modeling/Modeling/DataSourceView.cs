using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000067 RID: 103
	public sealed class DataSourceView : ModelingObject, IPersistable
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x0000DD95 File Offset: 0x0000BF95
		public DataSourceView()
			: this(new DataSourceView.DsvInfoDS())
		{
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000DDA2 File Offset: 0x0000BFA2
		private DataSourceView(DataSourceView.IDsvInfo dsvInfo)
		{
			this.Reset(dsvInfo);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000DDB1 File Offset: 0x0000BFB1
		public DataSourceView(string id, string name, string dataSourceID)
			: this()
		{
			this.m_id = id;
			this.m_name = name;
			this.m_dataSourceID = dataSourceID;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		private void Reset(DataSourceView.IDsvInfo dsvInfo)
		{
			base.CheckWriteable();
			this.m_id = string.Empty;
			this.m_name = string.Empty;
			this.m_description = string.Empty;
			this.m_createdTimestamp = default(DateTime);
			this.m_lastSchemaUpdate = default(DateTime);
			this.m_annotations = null;
			this.m_dataSourceID = string.Empty;
			this.m_dsvInfo = dsvInfo;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000DE35 File Offset: 0x0000C035
		public string ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000DE3D File Offset: 0x0000C03D
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000DE45 File Offset: 0x0000C045
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000DE4D File Offset: 0x0000C04D
		public DateTime CreatedTimestamp
		{
			get
			{
				return this.m_createdTimestamp;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000DE55 File Offset: 0x0000C055
		public DateTime LastSchemaUpdate
		{
			get
			{
				return this.m_lastSchemaUpdate;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000DE5D File Offset: 0x0000C05D
		public string DataSourceID
		{
			get
			{
				return this.m_dataSourceID;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000DE68 File Offset: 0x0000C068
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000DEDC File Offset: 0x0000C0DC
		public DsvCompareInfo CompareInfo
		{
			get
			{
				object obj = this.m_dsvInfo.ExtendedProperties["CompareInfo"];
				if (obj == null || obj is DsvCompareInfo)
				{
					return (DsvCompareInfo)obj;
				}
				if (base.IsCompiled)
				{
					throw new InternalModelingException("compareInfo is not DsvCompareInfo on compiled DataSourceView");
				}
				DsvCompareInfo dsvCompareInfo;
				try
				{
					dsvCompareInfo = DsvCompareInfo.FromXml(obj.ToString());
				}
				catch (XmlException)
				{
					dsvCompareInfo = null;
				}
				this.CompareInfo = dsvCompareInfo;
				return dsvCompareInfo;
			}
			set
			{
				base.CheckWriteable();
				DataSourceView.SetExtendedProperty(this.m_dsvInfo.ExtendedProperties, "CompareInfo", value);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000DEFA File Offset: 0x0000C0FA
		public DsvTableCollection Tables
		{
			get
			{
				return this.m_dsvInfo.Tables;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000DF07 File Offset: 0x0000C107
		public DsvRelationCollection Relations
		{
			get
			{
				return this.m_dsvInfo.Relations;
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000DF14 File Offset: 0x0000C114
		public static void SetExtendedProperty(IDictionary props, string propName, object propValue)
		{
			if (!props.Contains(propName))
			{
				if (propValue != null)
				{
					props.Add(propName, propValue);
				}
				return;
			}
			if (propValue == null)
			{
				props.Remove(propName);
				return;
			}
			props[propName] = propValue;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000DF3E File Offset: 0x0000C13E
		private static void CleanProperties(IDictionary properties)
		{
			properties.Remove("_ReadOnly");
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000DF4C File Offset: 0x0000C14C
		public void Load(XmlReader xr)
		{
			if (xr == null)
			{
				throw new ArgumentNullException("xr");
			}
			XmlUtil.WrapXmlExceptions(delegate
			{
				this.LoadCore(xr);
			}, ModelingErrorCode.InvalidDataSourceView, new XmlUtil.ErrorMessageWrap(SRErrors.InvalidDataSourceView));
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000DF9C File Offset: 0x0000C19C
		private void LoadCore(XmlReader xr)
		{
			XmlUtil.CheckElement(xr, "DataSourceView", "http://schemas.microsoft.com/analysisservices/2003/engine");
			if (xr.SchemaInfo != null)
			{
				XmlQualifiedName qualifiedName = xr.SchemaInfo.SchemaType.QualifiedName;
				if (qualifiedName != DataSourceView.DataSourceViewType)
				{
					throw new InternalModelingException(string.Concat(new string[] { "Unexpected DataSourceView type '", qualifiedName.Name, "' in namespace '", qualifiedName.Namespace, "'" }));
				}
			}
			this.Reset(new DataSourceView.DsvInfoDS());
			XmlUtil.LoadDirectChildren(xr, "DataSourceView", "http://schemas.microsoft.com/analysisservices/2003/engine", new XmlUtil.LoadXmlElementLDC(this.LoadXmlElement));
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000E044 File Offset: 0x0000C244
		private bool LoadXmlElement(XmlReader xr)
		{
			string localName = xr.LocalName;
			if (localName != null)
			{
				int length = localName.Length;
				if (length <= 11)
				{
					switch (length)
					{
					case 2:
						if (!(localName == "ID"))
						{
							return false;
						}
						this.m_id = xr.ReadElementContentAsString();
						break;
					case 3:
					case 5:
						return false;
					case 4:
						if (!(localName == "Name"))
						{
							return false;
						}
						this.m_name = xr.ReadElementContentAsString();
						break;
					case 6:
						if (!(localName == "Schema"))
						{
							return false;
						}
						if (xr.ReadToDescendant("schema", "http://www.w3.org/2001/XMLSchema"))
						{
							this.m_dsvInfo.Load(xr);
						}
						break;
					default:
					{
						if (length != 11)
						{
							return false;
						}
						char c = localName[0];
						if (c != 'A')
						{
							if (c != 'D')
							{
								return false;
							}
							if (!(localName == "Description"))
							{
								return false;
							}
							this.m_description = xr.ReadElementContentAsString();
						}
						else
						{
							if (!(localName == "Annotations"))
							{
								return false;
							}
							this.m_annotations = new XmlDocument();
							this.m_annotations.Load(xr.ReadSubtree());
						}
						break;
					}
					}
				}
				else if (length != 12)
				{
					if (length != 16)
					{
						return false;
					}
					char c = localName[0];
					if (c != 'C')
					{
						if (c != 'L')
						{
							return false;
						}
						if (!(localName == "LastSchemaUpdate"))
						{
							return false;
						}
						this.m_lastSchemaUpdate = DataSourceView.ReadDateTimeChecked(xr);
					}
					else
					{
						if (!(localName == "CreatedTimestamp"))
						{
							return false;
						}
						this.m_createdTimestamp = DataSourceView.ReadDateTimeChecked(xr);
					}
				}
				else
				{
					if (!(localName == "DataSourceID"))
					{
						return false;
					}
					this.m_dataSourceID = xr.ReadElementContentAsString();
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000E204 File Offset: 0x0000C404
		private static DateTime ReadDateTimeChecked(XmlReader xr)
		{
			DateTime dateTime;
			try
			{
				dateTime = xr.ReadElementContentAsDateTime();
			}
			catch (FormatException ex)
			{
				throw new XmlValidationException(ModelingErrorCode.InvalidDataSourceView, SRErrors.InvalidDataSourceView(ex.Message), xr as IXmlLineInfo);
			}
			return dateTime;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000E244 File Offset: 0x0000C444
		public void WriteTo(XmlWriter xw)
		{
			if (xw == null)
			{
				throw new ArgumentNullException("xw");
			}
			xw.WriteStartElement("DataSourceView", "http://schemas.microsoft.com/analysisservices/2003/engine");
			DataSourceView.WriteElementIfNonDefault<string>(xw, "ID", this.m_id);
			DataSourceView.WriteElementIfNonDefault<string>(xw, "Name", this.m_name);
			DataSourceView.WriteElementIfNonDefault<string>(xw, "Description", this.m_description);
			DataSourceView.WriteElementIfNonDefault<DateTime>(xw, "CreatedTimestamp", this.m_createdTimestamp);
			DataSourceView.WriteElementIfNonDefault<DateTime>(xw, "LastSchemaUpdate", this.m_lastSchemaUpdate);
			if (this.m_annotations != null)
			{
				this.m_annotations.WriteTo(xw);
			}
			DataSourceView.WriteElementIfNonDefault<string>(xw, "DataSourceID", this.m_dataSourceID);
			this.WriteSchema(xw);
			xw.WriteEndElement();
			xw.Flush();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		private void WriteSchema(XmlWriter xw)
		{
			xw.WriteStartElement("Schema", "http://schemas.microsoft.com/analysisservices/2003/engine");
			this.m_dsvInfo.WriteTo(xw);
			xw.WriteEndElement();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000E320 File Offset: 0x0000C520
		private static void WriteElementIfNonDefault<T>(XmlWriter xw, string elementName, T value)
		{
			if (value != null && !object.Equals(value, default(T)) && value as string != string.Empty)
			{
				xw.WriteStartElement(elementName, "http://schemas.microsoft.com/analysisservices/2003/engine");
				xw.WriteValue(value);
				xw.WriteEndElement();
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000E388 File Offset: 0x0000C588
		internal void Compile(CompilationContext ctx)
		{
			if (this.CompareInfo != null)
			{
				this.CompareInfo.Compile(ctx);
			}
			base.Compile(ctx.ShouldPersist);
			if (ctx.ShouldPersist)
			{
				this.m_dsvInfo.ExtendedProperties["_ReadOnly"] = true;
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		internal static DataSourceView FromBinary()
		{
			return new DataSourceView(null);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000E3E0 File Offset: 0x0000C5E0
		internal static IPersistable CreateDsvSchemaPersistable()
		{
			return new DataSourceView.DsvInfoBinary();
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000E3E7 File Offset: 0x0000C5E7
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			this.Serialize(writer);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(DataSourceView.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ID)
				{
					if (memberName != MemberName.Name)
					{
						switch (memberName)
						{
						case MemberName.CreatedTimeStamp:
							writer.Write(this.m_createdTimestamp);
							break;
						case MemberName.LastSchemaUpdate:
							writer.Write(this.m_lastSchemaUpdate);
							break;
						case MemberName.DataSourceID:
							writer.Write(this.m_dataSourceID);
							break;
						case MemberName.DsvSchema:
							writer.Write(this.m_dsvInfo);
							break;
						default:
							throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
						}
					}
					else
					{
						writer.Write(this.m_name);
					}
				}
				else
				{
					writer.Write(this.m_id);
				}
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000E4D5 File Offset: 0x0000C6D5
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(DataSourceView.Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ID)
				{
					if (memberName != MemberName.Name)
					{
						switch (memberName)
						{
						case MemberName.CreatedTimeStamp:
							this.m_createdTimestamp = reader.ReadDateTime();
							break;
						case MemberName.LastSchemaUpdate:
							this.m_lastSchemaUpdate = reader.ReadDateTime();
							break;
						case MemberName.DataSourceID:
							this.m_dataSourceID = reader.ReadString();
							break;
						case MemberName.DsvSchema:
							this.m_dsvInfo = (DataSourceView.IDsvInfo)reader.ReadRIFObject();
							break;
						default:
							throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
						}
					}
					else
					{
						this.m_name = reader.ReadString();
					}
				}
				else
				{
					this.m_id = reader.ReadString();
				}
			}
			if (base.IsCompiled)
			{
				using (base.AllowWriteOperations())
				{
					if (this.CompareInfo != null)
					{
						this.CompareInfo.Compile(new CompilationContext(true, false));
					}
				}
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000E610 File Offset: 0x0000C810
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000E61C File Offset: 0x0000C81C
		ObjectType IPersistable.GetObjectType()
		{
			return ObjectType.DataSourceView;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000E620 File Offset: 0x0000C820
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref DataSourceView.__declaration, DataSourceView.__declarationLock, () => new Declaration(ObjectType.DataSourceView, ObjectType.ModelingObject, new List<MemberInfo>
				{
					new MemberInfo(MemberName.ID, Token.String),
					new MemberInfo(MemberName.Name, Token.String),
					new MemberInfo(MemberName.CreatedTimeStamp, Token.DateTime),
					new MemberInfo(MemberName.LastSchemaUpdate, Token.DateTime),
					new MemberInfo(MemberName.DataSourceID, Token.String),
					new MemberInfo(MemberName.DsvSchema, ObjectType.DataSourceViewSchema)
				}));
			}
		}

		// Token: 0x04000245 RID: 581
		public const string Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine";

		// Token: 0x04000246 RID: 582
		internal const string DataSourceViewElem = "DataSourceView";

		// Token: 0x04000247 RID: 583
		private const string IdElem = "ID";

		// Token: 0x04000248 RID: 584
		private const string NameElem = "Name";

		// Token: 0x04000249 RID: 585
		private const string DescriptionElem = "Description";

		// Token: 0x0400024A RID: 586
		private const string CreatedTimestampElem = "CreatedTimestamp";

		// Token: 0x0400024B RID: 587
		private const string LastSchemaUpdateElem = "LastSchemaUpdate";

		// Token: 0x0400024C RID: 588
		private const string AnnotationsElem = "Annotations";

		// Token: 0x0400024D RID: 589
		private const string DataSourceIdElem = "DataSourceID";

		// Token: 0x0400024E RID: 590
		private const string SchemaElem = "Schema";

		// Token: 0x0400024F RID: 591
		private const string XmlSchemaElem = "schema";

		// Token: 0x04000250 RID: 592
		private const string XmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";

		// Token: 0x04000251 RID: 593
		private static readonly XmlQualifiedName DataSourceViewType = new XmlQualifiedName("DataSourceView", "http://schemas.microsoft.com/analysisservices/2003/engine");

		// Token: 0x04000252 RID: 594
		public const string CompareInfoExtProperty = "CompareInfo";

		// Token: 0x04000253 RID: 595
		private string m_id;

		// Token: 0x04000254 RID: 596
		private string m_name;

		// Token: 0x04000255 RID: 597
		private string m_description;

		// Token: 0x04000256 RID: 598
		private DateTime m_createdTimestamp;

		// Token: 0x04000257 RID: 599
		private DateTime m_lastSchemaUpdate;

		// Token: 0x04000258 RID: 600
		private XmlDocument m_annotations;

		// Token: 0x04000259 RID: 601
		private string m_dataSourceID;

		// Token: 0x0400025A RID: 602
		private DataSourceView.IDsvInfo m_dsvInfo;

		// Token: 0x0400025B RID: 603
		private static Declaration __declaration;

		// Token: 0x0400025C RID: 604
		private static readonly object __declarationLock = new object();

		// Token: 0x02000136 RID: 310
		private interface IDsvInfo : IPersistable
		{
			// Token: 0x17000322 RID: 802
			// (get) Token: 0x06000E14 RID: 3604
			bool IsReadOnly { get; }

			// Token: 0x17000323 RID: 803
			// (get) Token: 0x06000E15 RID: 3605
			DsvTableCollection Tables { get; }

			// Token: 0x17000324 RID: 804
			// (get) Token: 0x06000E16 RID: 3606
			DsvRelationCollection Relations { get; }

			// Token: 0x17000325 RID: 805
			// (get) Token: 0x06000E17 RID: 3607
			IDictionary ExtendedProperties { get; }

			// Token: 0x06000E18 RID: 3608
			void Load(XmlReader xr);

			// Token: 0x06000E19 RID: 3609
			void WriteTo(XmlWriter xw);
		}

		// Token: 0x02000137 RID: 311
		private sealed class DsvInfoDS : DataSourceView.IDsvInfo, IPersistable
		{
			// Token: 0x06000E1A RID: 3610 RVA: 0x0002DC63 File Offset: 0x0002BE63
			internal DsvInfoDS()
			{
				this.m_dataSet = new DataSet();
				this.m_tables = DsvTableCollection.FromDataRelationCollection(this.m_dataSet.Tables);
				this.m_relations = DsvRelationCollection.FromDataRelationCollection(this.m_dataSet.Relations);
			}

			// Token: 0x17000326 RID: 806
			// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0002DCA2 File Offset: 0x0002BEA2
			bool DataSourceView.IDsvInfo.IsReadOnly
			{
				get
				{
					return DsvItem.IsDataSetReadonly(this.m_dataSet);
				}
			}

			// Token: 0x17000327 RID: 807
			// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0002DCAF File Offset: 0x0002BEAF
			DsvTableCollection DataSourceView.IDsvInfo.Tables
			{
				get
				{
					return this.m_tables;
				}
			}

			// Token: 0x17000328 RID: 808
			// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0002DCB7 File Offset: 0x0002BEB7
			DsvRelationCollection DataSourceView.IDsvInfo.Relations
			{
				get
				{
					return this.m_relations;
				}
			}

			// Token: 0x17000329 RID: 809
			// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0002DCBF File Offset: 0x0002BEBF
			IDictionary DataSourceView.IDsvInfo.ExtendedProperties
			{
				get
				{
					return this.m_dataSet.ExtendedProperties;
				}
			}

			// Token: 0x06000E1F RID: 3615 RVA: 0x0002DCCC File Offset: 0x0002BECC
			void DataSourceView.IDsvInfo.Load(XmlReader xr)
			{
				this.m_dataSet.ReadXmlSchema(xr);
			}

			// Token: 0x06000E20 RID: 3616 RVA: 0x0002DCDA File Offset: 0x0002BEDA
			void DataSourceView.IDsvInfo.WriteTo(XmlWriter xw)
			{
				DataSet dataSet = this.m_dataSet.Clone();
				DataSourceView.DsvInfoDS.CleanProperties(dataSet);
				dataSet.WriteXmlSchema(xw);
			}

			// Token: 0x06000E21 RID: 3617 RVA: 0x0002DCF4 File Offset: 0x0002BEF4
			private static void CleanProperties(DataSet ds)
			{
				DataSourceView.CleanProperties(ds.ExtendedProperties);
				foreach (object obj in ds.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					DsvTable.CleanProperties(dataTable.ExtendedProperties);
					foreach (object obj2 in dataTable.Columns)
					{
						DsvColumn.CleanProperties(((DataColumn)obj2).ExtendedProperties);
					}
					foreach (object obj3 in dataTable.Constraints)
					{
						DsvItem.CleanProperties(((Constraint)obj3).ExtendedProperties);
					}
				}
				foreach (object obj4 in ds.Relations)
				{
					DsvItem.CleanProperties(((DataRelation)obj4).ExtendedProperties);
				}
			}

			// Token: 0x06000E22 RID: 3618 RVA: 0x0002DE40 File Offset: 0x0002C040
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				((IPersistable)new DataSourceView.DsvInfoBinary(this)).Serialize(writer);
			}

			// Token: 0x06000E23 RID: 3619 RVA: 0x0002DE4E File Offset: 0x0002C04E
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				throw new InternalModelingException("Deserialize is not supported.");
			}

			// Token: 0x06000E24 RID: 3620 RVA: 0x0002DE5A File Offset: 0x0002C05A
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000E25 RID: 3621 RVA: 0x0002DE66 File Offset: 0x0002C066
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DataSourceViewSchema;
			}

			// Token: 0x040005E9 RID: 1513
			private readonly DataSet m_dataSet;

			// Token: 0x040005EA RID: 1514
			private readonly DsvTableCollection m_tables;

			// Token: 0x040005EB RID: 1515
			private readonly DsvRelationCollection m_relations;
		}

		// Token: 0x02000138 RID: 312
		private sealed class DsvInfoBinary : DataSourceView.IDsvInfo, IPersistable
		{
			// Token: 0x06000E26 RID: 3622 RVA: 0x0002DE6A File Offset: 0x0002C06A
			internal DsvInfoBinary()
			{
			}

			// Token: 0x06000E27 RID: 3623 RVA: 0x0002DE72 File Offset: 0x0002C072
			internal DsvInfoBinary(DataSourceView.IDsvInfo dsvInfo)
			{
				this.m_tables = dsvInfo.Tables;
				this.m_relations = dsvInfo.Relations;
				this.m_extendedProperties = dsvInfo.ExtendedProperties;
			}

			// Token: 0x1700032A RID: 810
			// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0002DE9E File Offset: 0x0002C09E
			bool DataSourceView.IDsvInfo.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700032B RID: 811
			// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0002DEA1 File Offset: 0x0002C0A1
			DsvTableCollection DataSourceView.IDsvInfo.Tables
			{
				get
				{
					return this.m_tables;
				}
			}

			// Token: 0x1700032C RID: 812
			// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0002DEA9 File Offset: 0x0002C0A9
			DsvRelationCollection DataSourceView.IDsvInfo.Relations
			{
				get
				{
					return this.m_relations;
				}
			}

			// Token: 0x1700032D RID: 813
			// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0002DEB1 File Offset: 0x0002C0B1
			IDictionary DataSourceView.IDsvInfo.ExtendedProperties
			{
				get
				{
					return this.m_extendedProperties;
				}
			}

			// Token: 0x06000E2C RID: 3628 RVA: 0x0002DEB9 File Offset: 0x0002C0B9
			void DataSourceView.IDsvInfo.Load(XmlReader xr)
			{
				throw new InternalModelingException("Load(XmlReader) is not supported.");
			}

			// Token: 0x06000E2D RID: 3629 RVA: 0x0002DEC5 File Offset: 0x0002C0C5
			void DataSourceView.IDsvInfo.WriteTo(XmlWriter xw)
			{
				throw new InternalModelingException("WriteTo(XmlWriter) is not supported.");
			}

			// Token: 0x06000E2E RID: 3630 RVA: 0x0002DED4 File Offset: 0x0002C0D4
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(DataSourceView.DsvInfoBinary.Declaration);
				while (writer.NextMember())
				{
					MemberName memberName = writer.CurrentMember.MemberName;
					if (memberName != MemberName.ExtendedProperties)
					{
						if (memberName != MemberName.Tables)
						{
							if (memberName != MemberName.Relations)
							{
								throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
							}
							writer.Write(this.m_relations);
						}
						else
						{
							writer.Write(this.m_tables);
						}
					}
					else
					{
						PersistenceHelper.WriteProperyCollection(ref writer, this.m_extendedProperties, new Action<IDictionary>(DataSourceView.CleanProperties));
					}
				}
			}

			// Token: 0x06000E2F RID: 3631 RVA: 0x0002DF80 File Offset: 0x0002C180
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(DataSourceView.DsvInfoBinary.Declaration);
				while (reader.NextMember())
				{
					MemberName memberName = reader.CurrentMember.MemberName;
					if (memberName != MemberName.ExtendedProperties)
					{
						if (memberName != MemberName.Tables)
						{
							if (memberName != MemberName.Relations)
							{
								throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
							}
							this.m_relations = (DsvRelationCollection)reader.ReadRIFObject();
						}
						else
						{
							this.m_tables = (DsvTableCollection)reader.ReadRIFObject();
						}
					}
					else
					{
						this.m_extendedProperties = PersistenceHelper.ReadPropertyCollection(ref reader, (string name) => string.CompareOrdinal(name, "CompareInfo") == 0);
					}
				}
			}

			// Token: 0x06000E30 RID: 3632 RVA: 0x0002E048 File Offset: 0x0002C248
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000E31 RID: 3633 RVA: 0x0002E054 File Offset: 0x0002C254
			ObjectType IPersistable.GetObjectType()
			{
				return ObjectType.DataSourceViewSchema;
			}

			// Token: 0x1700032E RID: 814
			// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0002E058 File Offset: 0x0002C258
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref DataSourceView.DsvInfoBinary.__declaration, DataSourceView.DsvInfoBinary.__declarationLock, () => new Declaration(ObjectType.DataSourceViewSchema, ObjectType.None, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Tables, ObjectType.DsvTableCollection),
						new MemberInfo(MemberName.Relations, ObjectType.DsvRelationCollection),
						new MemberInfo(MemberName.ExtendedProperties, ObjectType.StringObjectHashtable, Token.String)
					}));
				}
			}

			// Token: 0x040005EC RID: 1516
			private DsvTableCollection m_tables;

			// Token: 0x040005ED RID: 1517
			private DsvRelationCollection m_relations;

			// Token: 0x040005EE RID: 1518
			private IDictionary m_extendedProperties;

			// Token: 0x040005EF RID: 1519
			private static Declaration __declaration;

			// Token: 0x040005F0 RID: 1520
			private static readonly object __declarationLock = new object();
		}
	}
}
