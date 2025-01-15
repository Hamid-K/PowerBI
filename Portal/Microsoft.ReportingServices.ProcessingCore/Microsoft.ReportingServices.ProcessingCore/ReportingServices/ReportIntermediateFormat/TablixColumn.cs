using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200051F RID: 1311
	internal sealed class TablixColumn : IDOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060046B2 RID: 18098 RVA: 0x001294A1 File Offset: 0x001276A1
		internal TablixColumn()
		{
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x001294A9 File Offset: 0x001276A9
		internal TablixColumn(int id)
			: base(id)
		{
		}

		// Token: 0x17001D7C RID: 7548
		// (get) Token: 0x060046B4 RID: 18100 RVA: 0x001294B2 File Offset: 0x001276B2
		// (set) Token: 0x060046B5 RID: 18101 RVA: 0x001294BA File Offset: 0x001276BA
		internal string Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x17001D7D RID: 7549
		// (get) Token: 0x060046B6 RID: 18102 RVA: 0x001294C3 File Offset: 0x001276C3
		// (set) Token: 0x060046B7 RID: 18103 RVA: 0x001294CB File Offset: 0x001276CB
		internal double WidthValue
		{
			get
			{
				return this.m_widthValue;
			}
			set
			{
				this.m_widthValue = value;
			}
		}

		// Token: 0x17001D7E RID: 7550
		// (get) Token: 0x060046B8 RID: 18104 RVA: 0x001294D4 File Offset: 0x001276D4
		// (set) Token: 0x060046B9 RID: 18105 RVA: 0x001294DC File Offset: 0x001276DC
		internal bool ForAutoSubtotal
		{
			get
			{
				return this.m_forAutoSubtotal;
			}
			set
			{
				this.m_forAutoSubtotal = value;
			}
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x001294E5 File Offset: 0x001276E5
		internal void Initialize(InitializationContext context)
		{
			this.m_widthValue = context.ValidateSize(this.m_width, "Width");
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x00129500 File Offset: 0x00127700
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			TablixColumn tablixColumn = (TablixColumn)base.PublishClone(context);
			if (this.m_width != null)
			{
				tablixColumn.m_width = (string)this.m_width.Clone();
			}
			return tablixColumn;
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x0012953C File Offset: 0x0012773C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixColumn, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Width, Token.String),
				new MemberInfo(MemberName.WidthValue, Token.Double)
			});
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x0012958C File Offset: 0x0012778C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(TablixColumn.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Width)
				{
					if (memberName != MemberName.WidthValue)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_widthValue);
					}
				}
				else
				{
					writer.Write(this.m_width);
				}
			}
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x00129600 File Offset: 0x00127800
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(TablixColumn.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Width)
				{
					if (memberName != MemberName.WidthValue)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_widthValue = reader.ReadDouble();
					}
				}
				else
				{
					this.m_width = reader.ReadString();
				}
			}
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x00129673 File Offset: 0x00127873
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x00129680 File Offset: 0x00127880
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixColumn;
		}

		// Token: 0x04001FB1 RID: 8113
		private string m_width;

		// Token: 0x04001FB2 RID: 8114
		private double m_widthValue;

		// Token: 0x04001FB3 RID: 8115
		[NonSerialized]
		private bool m_forAutoSubtotal;

		// Token: 0x04001FB4 RID: 8116
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = TablixColumn.GetDeclaration();
	}
}
