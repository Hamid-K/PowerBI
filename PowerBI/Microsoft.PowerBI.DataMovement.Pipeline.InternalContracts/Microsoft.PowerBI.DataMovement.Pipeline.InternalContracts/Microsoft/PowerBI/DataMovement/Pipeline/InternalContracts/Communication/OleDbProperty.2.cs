using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000053 RID: 83
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal class OleDbProperty<[Nullable(2)] TValue> : OleDbProperty
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00002ED6 File Offset: 0x000010D6
		internal OleDbProperty()
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00002EDE File Offset: 0x000010DE
		internal OleDbProperty(Guid propertyGroup, DBPROPID propertyId, bool required, TValue value)
		{
			base.PropertyGroup = propertyGroup;
			base.PropertyId = propertyId;
			base.Required = required;
			this.m_value = value;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00002F03 File Offset: 0x00001103
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00002F10 File Offset: 0x00001110
		[IgnoreDataMember]
		internal override object Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = (TValue)((object)value);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00002F1E File Offset: 0x0000111E
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00002F26 File Offset: 0x00001126
		[DataMember(Name = "value", IsRequired = true, EmitDefaultValue = true)]
		internal TValue TypedValue
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

		// Token: 0x060001A0 RID: 416 RVA: 0x00002F2F File Offset: 0x0000112F
		internal override OleDbProperty Clone()
		{
			return new OleDbProperty<TValue>
			{
				PropertyGroup = base.PropertyGroup,
				PropertyId = base.PropertyId,
				Required = base.Required,
				Value = this.Value
			};
		}

		// Token: 0x040000DB RID: 219
		[IgnoreDataMember]
		private TValue m_value;
	}
}
