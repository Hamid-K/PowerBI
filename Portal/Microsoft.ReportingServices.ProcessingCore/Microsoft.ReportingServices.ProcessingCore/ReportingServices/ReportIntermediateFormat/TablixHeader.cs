using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200051E RID: 1310
	[Serializable]
	internal sealed class TablixHeader : IDOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600469C RID: 18076 RVA: 0x00128F24 File Offset: 0x00127124
		internal TablixHeader()
		{
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x00128F2C File Offset: 0x0012712C
		internal TablixHeader(int id)
			: base(id)
		{
		}

		// Token: 0x17001D76 RID: 7542
		// (get) Token: 0x0600469E RID: 18078 RVA: 0x00128F35 File Offset: 0x00127135
		// (set) Token: 0x0600469F RID: 18079 RVA: 0x00128F3D File Offset: 0x0012713D
		internal string Size
		{
			get
			{
				return this.m_size;
			}
			set
			{
				this.m_size = value;
			}
		}

		// Token: 0x17001D77 RID: 7543
		// (get) Token: 0x060046A0 RID: 18080 RVA: 0x00128F46 File Offset: 0x00127146
		// (set) Token: 0x060046A1 RID: 18081 RVA: 0x00128F4E File Offset: 0x0012714E
		internal double SizeValue
		{
			get
			{
				return this.m_sizeValue;
			}
			set
			{
				this.m_sizeValue = value;
			}
		}

		// Token: 0x17001D78 RID: 7544
		// (get) Token: 0x060046A2 RID: 18082 RVA: 0x00128F57 File Offset: 0x00127157
		// (set) Token: 0x060046A3 RID: 18083 RVA: 0x00128F5F File Offset: 0x0012715F
		internal ReportSize SizeForRendering
		{
			get
			{
				return this.m_sizeForRendering;
			}
			set
			{
				this.m_sizeForRendering = value;
			}
		}

		// Token: 0x17001D79 RID: 7545
		// (get) Token: 0x060046A4 RID: 18084 RVA: 0x00128F68 File Offset: 0x00127168
		// (set) Token: 0x060046A5 RID: 18085 RVA: 0x00128F70 File Offset: 0x00127170
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem CellContents
		{
			get
			{
				return this.m_cellContents;
			}
			set
			{
				this.m_cellContents = value;
			}
		}

		// Token: 0x17001D7A RID: 7546
		// (get) Token: 0x060046A6 RID: 18086 RVA: 0x00128F79 File Offset: 0x00127179
		// (set) Token: 0x060046A7 RID: 18087 RVA: 0x00128F81 File Offset: 0x00127181
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem AltCellContents
		{
			get
			{
				return this.m_altCellContents;
			}
			set
			{
				this.m_altCellContents = value;
			}
		}

		// Token: 0x17001D7B RID: 7547
		// (get) Token: 0x060046A8 RID: 18088 RVA: 0x00128F8C File Offset: 0x0012718C
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> CellContentCollection
		{
			get
			{
				if (this.m_cellContentCollection == null && this.m_cellContents != null)
				{
					this.m_cellContentCollection = new List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem>((this.m_altCellContents == null) ? 1 : 2);
					if (this.m_cellContents != null)
					{
						this.m_cellContentCollection.Add(this.m_cellContents);
					}
					if (this.m_altCellContents != null)
					{
						this.m_cellContentCollection.Add(this.m_altCellContents);
					}
				}
				return this.m_cellContentCollection;
			}
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x00128FF8 File Offset: 0x001271F8
		internal void Initialize(InitializationContext context, bool isColumn, bool ignoreSize)
		{
			if (this.m_cellContents != null)
			{
				this.m_cellContents.Initialize(context);
				if (this.m_altCellContents != null)
				{
					this.m_altCellContents.Initialize(context);
				}
			}
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x00129024 File Offset: 0x00127224
		internal object PublishClone(AutomaticSubtotalContext context, bool isClonedDynamic)
		{
			TablixHeader tablixHeader = (TablixHeader)base.PublishClone(context);
			if (this.m_size != null)
			{
				tablixHeader.m_size = (string)this.m_size.Clone();
			}
			if (this.m_cellContents != null)
			{
				if (isClonedDynamic)
				{
					ExpressionInfo expressionInfo = null;
					Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle rectangle = new Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle(context.GenerateID(), context.GenerateID(), context.CurrentDataRegion);
					rectangle.Name = context.CreateUniqueReportItemName(this.m_cellContents.Name, true, false);
					Microsoft.ReportingServices.ReportIntermediateFormat.Style styleClass = this.m_cellContents.StyleClass;
					if (styleClass != null)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Style style = new Microsoft.ReportingServices.ReportIntermediateFormat.Style(ConstructionPhase.Publishing);
						foreach (string text in TablixHeader.m_StylesForEmptyRectangleInSubtotals)
						{
							this.AddAttribute(context, styleClass, style, text, expressionInfo);
						}
						rectangle.StyleClass = style;
					}
					tablixHeader.m_cellContents = rectangle;
				}
				else
				{
					tablixHeader.m_cellContents = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem)this.m_cellContents.PublishClone(context);
					if (this.m_altCellContents != null)
					{
						Global.Tracer.Assert(tablixHeader.m_cellContents is Microsoft.ReportingServices.ReportIntermediateFormat.CustomReportItem);
						tablixHeader.m_altCellContents = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem)this.m_altCellContents.PublishClone(context);
						((Microsoft.ReportingServices.ReportIntermediateFormat.CustomReportItem)tablixHeader.m_cellContents).AltReportItem = tablixHeader.m_altCellContents;
					}
				}
			}
			return tablixHeader;
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x00129160 File Offset: 0x00127360
		private void AddAttribute(AutomaticSubtotalContext context, Microsoft.ReportingServices.ReportIntermediateFormat.Style originalStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Style newStyle, string name, ExpressionInfo meDotValueExpression)
		{
			AttributeInfo attributeInfo;
			if (originalStyle.GetAttributeInfo(name, out attributeInfo))
			{
				if (attributeInfo.IsExpression)
				{
					newStyle.AddAttribute(name, (ExpressionInfo)originalStyle.ExpressionList[attributeInfo.IntValue].PublishClone(context));
					return;
				}
				newStyle.StyleAttributes.Add(name, attributeInfo.PublishClone(context));
			}
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x001291BC File Offset: 0x001273BC
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixHeader, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Size, Token.String),
				new MemberInfo(MemberName.SizeValue, Token.Double),
				new MemberInfo(MemberName.CellContents, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem),
				new MemberInfo(MemberName.AltCellContents, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem)
			});
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x00129234 File Offset: 0x00127434
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(TablixHeader.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.SizeValue)
				{
					if (memberName == MemberName.Size)
					{
						writer.Write(this.m_size);
						continue;
					}
					if (memberName == MemberName.SizeValue)
					{
						writer.Write(this.m_sizeValue);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.CellContents)
					{
						writer.Write(this.m_cellContents);
						continue;
					}
					if (memberName == MemberName.AltCellContents)
					{
						writer.Write(this.m_altCellContents);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x001292E8 File Offset: 0x001274E8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(TablixHeader.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.SizeValue)
				{
					if (memberName == MemberName.Size)
					{
						this.m_size = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.SizeValue)
					{
						this.m_sizeValue = reader.ReadDouble();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.CellContents)
					{
						this.m_cellContents = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.AltCellContents)
					{
						this.m_altCellContents = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x001293A3 File Offset: 0x001275A3
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x001293B0 File Offset: 0x001275B0
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixHeader;
		}

		// Token: 0x04001FA9 RID: 8105
		private string m_size;

		// Token: 0x04001FAA RID: 8106
		private double m_sizeValue;

		// Token: 0x04001FAB RID: 8107
		private Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem m_cellContents;

		// Token: 0x04001FAC RID: 8108
		private Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem m_altCellContents;

		// Token: 0x04001FAD RID: 8109
		[NonSerialized]
		private static readonly string[] m_StylesForEmptyRectangleInSubtotals = new string[]
		{
			"BackgroundColor", "BackgroundGradientType", "BackgroundGradientEndColor", "BackgroundImageMIMEType", "BackgroundImageSource", "BackgroundImageValue", "BackgroundImage", "BackgroundRepeat", "BorderColor", "BorderColorTop",
			"BorderColorBottom", "BorderColorRight", "BorderColorLeft", "BorderStyle", "BorderStyleTop", "BorderStyleBottom", "BorderStyleLeft", "BorderStyleRight", "BorderWidth", "BorderWidthTop",
			"BorderWidthBottom", "BorderWidthLeft", "BorderWidthRight"
		};

		// Token: 0x04001FAE RID: 8110
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = TablixHeader.GetDeclaration();

		// Token: 0x04001FAF RID: 8111
		[NonSerialized]
		private ReportSize m_sizeForRendering;

		// Token: 0x04001FB0 RID: 8112
		[NonSerialized]
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> m_cellContentCollection;
	}
}
