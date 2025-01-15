using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200044B RID: 1099
	[Serializable]
	internal sealed class MapField : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060031DF RID: 12767 RVA: 0x000DF3E7 File Offset: 0x000DD5E7
		internal MapField()
		{
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000DF3EF File Offset: 0x000DD5EF
		internal MapField(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x170016E1 RID: 5857
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x000DF3FE File Offset: 0x000DD5FE
		// (set) Token: 0x060031E2 RID: 12770 RVA: 0x000DF406 File Offset: 0x000DD606
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

		// Token: 0x170016E2 RID: 5858
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x000DF40F File Offset: 0x000DD60F
		// (set) Token: 0x060031E4 RID: 12772 RVA: 0x000DF417 File Offset: 0x000DD617
		internal string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x170016E3 RID: 5859
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x000DF420 File Offset: 0x000DD620
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000DF42D File Offset: 0x000DD62D
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapField mapField = (MapField)base.MemberwiseClone();
			mapField.m_map = context.CurrentMapClone;
			return mapField;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000DF448 File Offset: 0x000DD648
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapField, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Value, Token.String),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference)
			});
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000DF4A4 File Offset: 0x000DD6A4
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapField.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					if (memberName != MemberName.Value)
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
						writer.Write(this.m_value);
					}
				}
				else
				{
					writer.Write(this.m_name);
				}
			}
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000DF520 File Offset: 0x000DD720
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapField.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					if (memberName != MemberName.Value)
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
						this.m_value = reader.ReadString();
					}
				}
				else
				{
					this.m_name = reader.ReadString();
				}
			}
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000DF59C File Offset: 0x000DD79C
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapField.m_Declaration.ObjectType, out list))
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

		// Token: 0x060031EB RID: 12779 RVA: 0x000DF640 File Offset: 0x000DD840
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapField;
		}

		// Token: 0x04001955 RID: 6485
		[Reference]
		private Map m_map;

		// Token: 0x04001956 RID: 6486
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapField.GetDeclaration();

		// Token: 0x04001957 RID: 6487
		private string m_name;

		// Token: 0x04001958 RID: 6488
		private string m_value;
	}
}
