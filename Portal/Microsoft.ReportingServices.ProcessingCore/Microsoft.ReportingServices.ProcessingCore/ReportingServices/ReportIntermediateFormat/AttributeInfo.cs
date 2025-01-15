using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000517 RID: 1303
	[Serializable]
	public sealed class AttributeInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001D06 RID: 7430
		// (get) Token: 0x06004549 RID: 17737 RVA: 0x00122827 File Offset: 0x00120A27
		// (set) Token: 0x0600454A RID: 17738 RVA: 0x0012282F File Offset: 0x00120A2F
		internal bool IsExpression
		{
			get
			{
				return this.m_isExpression;
			}
			set
			{
				this.m_isExpression = value;
			}
		}

		// Token: 0x17001D07 RID: 7431
		// (get) Token: 0x0600454B RID: 17739 RVA: 0x00122838 File Offset: 0x00120A38
		// (set) Token: 0x0600454C RID: 17740 RVA: 0x00122840 File Offset: 0x00120A40
		internal string Value
		{
			get
			{
				return this.m_stringValue;
			}
			set
			{
				this.m_stringValue = value;
			}
		}

		// Token: 0x17001D08 RID: 7432
		// (get) Token: 0x0600454D RID: 17741 RVA: 0x00122849 File Offset: 0x00120A49
		// (set) Token: 0x0600454E RID: 17742 RVA: 0x00122851 File Offset: 0x00120A51
		internal bool BoolValue
		{
			get
			{
				return this.m_boolValue;
			}
			set
			{
				this.m_boolValue = value;
			}
		}

		// Token: 0x17001D09 RID: 7433
		// (get) Token: 0x0600454F RID: 17743 RVA: 0x0012285A File Offset: 0x00120A5A
		// (set) Token: 0x06004550 RID: 17744 RVA: 0x00122862 File Offset: 0x00120A62
		internal int IntValue
		{
			get
			{
				return this.m_intValue;
			}
			set
			{
				this.m_intValue = value;
			}
		}

		// Token: 0x17001D0A RID: 7434
		// (get) Token: 0x06004551 RID: 17745 RVA: 0x0012286B File Offset: 0x00120A6B
		// (set) Token: 0x06004552 RID: 17746 RVA: 0x00122873 File Offset: 0x00120A73
		internal double FloatValue
		{
			get
			{
				return this.m_floatValue;
			}
			set
			{
				this.m_floatValue = value;
			}
		}

		// Token: 0x17001D0B RID: 7435
		// (get) Token: 0x06004553 RID: 17747 RVA: 0x0012287C File Offset: 0x00120A7C
		// (set) Token: 0x06004554 RID: 17748 RVA: 0x00122884 File Offset: 0x00120A84
		internal ValueType ValueType
		{
			get
			{
				return this.m_valueType;
			}
			set
			{
				this.m_valueType = value;
			}
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x00122890 File Offset: 0x00120A90
		internal AttributeInfo PublishClone(AutomaticSubtotalContext context)
		{
			AttributeInfo attributeInfo = (AttributeInfo)base.MemberwiseClone();
			if (this.m_stringValue != null)
			{
				attributeInfo.m_stringValue = (string)this.m_stringValue.Clone();
			}
			return attributeInfo;
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x001228C8 File Offset: 0x00120AC8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AttributeInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.IsExpression, Token.Boolean),
				new MemberInfo(MemberName.StringValue, Token.String),
				new MemberInfo(MemberName.BoolValue, Token.Boolean),
				new MemberInfo(MemberName.IntValue, Token.Int32),
				new MemberInfo(MemberName.FloatValue, Token.Double, Lifetime.AddedIn(200)),
				new MemberInfo(MemberName.ValueType, Token.Enum, Lifetime.AddedIn(200))
			});
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x00122978 File Offset: 0x00120B78
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(AttributeInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.IsExpression:
					writer.Write(this.m_isExpression);
					break;
				case MemberName.StringValue:
					writer.Write(this.m_stringValue);
					break;
				case MemberName.BoolValue:
					writer.Write(this.m_boolValue);
					break;
				case MemberName.IntValue:
					writer.Write(this.m_intValue);
					break;
				default:
					if (memberName != MemberName.FloatValue)
					{
						if (memberName != MemberName.ValueType)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.WriteEnum((int)this.m_valueType);
						}
					}
					else
					{
						writer.Write(this.m_floatValue);
					}
					break;
				}
			}
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x00122A44 File Offset: 0x00120C44
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(AttributeInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.IsExpression:
					this.m_isExpression = reader.ReadBoolean();
					break;
				case MemberName.StringValue:
					this.m_stringValue = reader.ReadString();
					break;
				case MemberName.BoolValue:
					this.m_boolValue = reader.ReadBoolean();
					break;
				case MemberName.IntValue:
					this.m_intValue = reader.ReadInt32();
					break;
				default:
					if (memberName != MemberName.FloatValue)
					{
						if (memberName != MemberName.ValueType)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_valueType = (ValueType)reader.ReadEnum();
						}
					}
					else
					{
						this.m_floatValue = reader.ReadDouble();
					}
					break;
				}
			}
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x00122B0E File Offset: 0x00120D0E
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x00122B1B File Offset: 0x00120D1B
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AttributeInfo;
		}

		// Token: 0x04001F3B RID: 7995
		private bool m_isExpression;

		// Token: 0x04001F3C RID: 7996
		private string m_stringValue;

		// Token: 0x04001F3D RID: 7997
		private bool m_boolValue;

		// Token: 0x04001F3E RID: 7998
		private int m_intValue;

		// Token: 0x04001F3F RID: 7999
		private double m_floatValue;

		// Token: 0x04001F40 RID: 8000
		private ValueType m_valueType;

		// Token: 0x04001F41 RID: 8001
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = AttributeInfo.GetDeclaration();
	}
}
