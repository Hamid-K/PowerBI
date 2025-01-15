using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000042 RID: 66
	public sealed class CustomProperty : ICloneable, IPersistable
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000A2A9 File Offset: 0x000084A9
		public CustomProperty(QName name)
			: this(name, null)
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A2B3 File Offset: 0x000084B3
		public CustomProperty(QName name, object value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000A2C9 File Offset: 0x000084C9
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000A2D1 File Offset: 0x000084D1
		public QName Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.CheckWriteable();
				this.m_name = QName.Verify(value);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000A2E5 File Offset: 0x000084E5
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000A2ED File Offset: 0x000084ED
		public object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				if (value is QName)
				{
					QName.Verify((QName)value);
				}
				this.CheckWriteable();
				this.m_value = value;
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000A310 File Offset: 0x00008510
		public override bool Equals(object obj)
		{
			CustomProperty customProperty = obj as CustomProperty;
			return customProperty != null && this.m_name.Equals(customProperty.Name) && object.Equals(this.m_value, customProperty.Value);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000A34F File Offset: 0x0000854F
		public override int GetHashCode()
		{
			return this.m_name.GetHashCode();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000A362 File Offset: 0x00008562
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{{CustomProperty Name={0}, Value={1}}}", new object[]
			{
				this.m_name,
				this.m_value ?? "[NULL]"
			});
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000A394 File Offset: 0x00008594
		public CustomProperty Clone()
		{
			return new CustomProperty(this.m_name, this.m_value);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000A3A7 File Offset: 0x000085A7
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000A3AF File Offset: 0x000085AF
		internal void MakeReadOnly()
		{
			this.m_readOnly = true;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000A3B8 File Offset: 0x000085B8
		private void CheckWriteable()
		{
			if (this.m_readOnly)
			{
				throw new InvalidOperationException(DevExceptionMessages.ReadOnly);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000A3CD File Offset: 0x000085CD
		internal CustomProperty()
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000A3D8 File Offset: 0x000085D8
		void IPersistable.Serialize(IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(CustomProperty.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Value)
				{
					if (memberName != MemberName.ReadOnly)
					{
						if (!PersistenceHelper.WriteQName(ref writer, this.m_name))
						{
							throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
						}
					}
					else
					{
						writer.Write(this.m_readOnly);
					}
				}
				else
				{
					writer.Write(this.m_value);
				}
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000A46C File Offset: 0x0000866C
		void IPersistable.Deserialize(IntermediateFormatReader reader)
		{
			this.Deserialize(reader);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000A478 File Offset: 0x00008678
		internal void Deserialize(IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(CustomProperty.Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Value)
				{
					if (memberName != MemberName.ReadOnly)
					{
						if (!PersistenceHelper.ReadQName(ref reader, ref this.m_name))
						{
							throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
						}
					}
					else
					{
						this.m_readOnly = reader.ReadBoolean();
					}
				}
				else
				{
					this.m_value = reader.ReadVariant();
				}
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000A50C File Offset: 0x0000870C
		void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			throw new InternalModelingException("ResolveReferences is not supported.");
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000A518 File Offset: 0x00008718
		ObjectType IPersistable.GetObjectType()
		{
			return ObjectType.CustomProperty;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000A51C File Offset: 0x0000871C
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref CustomProperty.__declaration, CustomProperty.__declarationLock, delegate
				{
					List<MemberInfo> list = new List<MemberInfo>();
					PersistenceHelper.DeclareQName(list);
					list.Add(new MemberInfo(MemberName.Value, Token.Object));
					list.Add(new MemberInfo(MemberName.ReadOnly, Token.Boolean));
					return new Declaration(ObjectType.CustomProperty, ObjectType.None, list);
				});
			}
		}

		// Token: 0x0400015F RID: 351
		private QName m_name;

		// Token: 0x04000160 RID: 352
		private object m_value;

		// Token: 0x04000161 RID: 353
		private bool m_readOnly;

		// Token: 0x04000162 RID: 354
		private static Declaration __declaration;

		// Token: 0x04000163 RID: 355
		private static readonly object __declarationLock = new object();
	}
}
