using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200042A RID: 1066
	[Serializable]
	internal sealed class MapFieldDefinition : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002F37 RID: 12087 RVA: 0x000D5EA7 File Offset: 0x000D40A7
		internal MapFieldDefinition()
		{
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x000D5EAF File Offset: 0x000D40AF
		internal MapFieldDefinition(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x000D5EBE File Offset: 0x000D40BE
		// (set) Token: 0x06002F3A RID: 12090 RVA: 0x000D5EC6 File Offset: 0x000D40C6
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x000D5ECF File Offset: 0x000D40CF
		// (set) Token: 0x06002F3C RID: 12092 RVA: 0x000D5ED7 File Offset: 0x000D40D7
		internal MapDataType DataType
		{
			get
			{
				return this.m_dataType;
			}
			set
			{
				this.m_dataType = value;
			}
		}

		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x06002F3D RID: 12093 RVA: 0x000D5EE0 File Offset: 0x000D40E0
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000D5EED File Offset: 0x000D40ED
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapFieldDefinition mapFieldDefinition = (MapFieldDefinition)base.MemberwiseClone();
			mapFieldDefinition.m_map = context.CurrentMapClone;
			return mapFieldDefinition;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x000D5F08 File Offset: 0x000D4108
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapFieldDefinition, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.DataType, Token.Enum),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference)
			});
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000D5F60 File Offset: 0x000D4160
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapFieldDefinition.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					if (memberName != MemberName.DataType)
					{
						if (memberName == MemberName.Map)
						{
							writer.WriteReference(this.m_map);
						}
						else
						{
							Global.Tracer.Assert(false);
						}
					}
					else
					{
						writer.WriteEnum((int)this.m_dataType);
					}
				}
				else
				{
					writer.Write(this.m_name);
				}
			}
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000D5FDC File Offset: 0x000D41DC
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapFieldDefinition.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					if (memberName != MemberName.DataType)
					{
						if (memberName == MemberName.Map)
						{
							this.m_map = reader.ReadReference<Map>(this);
						}
						else
						{
							Global.Tracer.Assert(false);
						}
					}
					else
					{
						this.m_dataType = (MapDataType)reader.ReadEnum();
					}
				}
				else
				{
					this.m_name = reader.ReadString();
				}
			}
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000D6058 File Offset: 0x000D4258
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapFieldDefinition.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.Map)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_map = (Map)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000D60FC File Offset: 0x000D42FC
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapFieldDefinition;
		}

		// Token: 0x040018A9 RID: 6313
		[Reference]
		private Map m_map;

		// Token: 0x040018AA RID: 6314
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapFieldDefinition.GetDeclaration();

		// Token: 0x040018AB RID: 6315
		private string m_name;

		// Token: 0x040018AC RID: 6316
		private MapDataType m_dataType;
	}
}
