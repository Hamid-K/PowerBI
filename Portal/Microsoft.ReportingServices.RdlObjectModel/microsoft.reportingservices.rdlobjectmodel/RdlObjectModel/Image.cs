using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel.ExpressionParser;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000172 RID: 370
	public class Image : ReportItem
	{
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0002070A File Offset: 0x0001E90A
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x00020719 File Offset: 0x0001E919
		public SourceType Source
		{
			get
			{
				return (SourceType)base.PropertyStore.GetInteger(18);
			}
			set
			{
				base.PropertyStore.SetInteger(18, (int)value);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00020729 File Offset: 0x0001E929
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x00020738 File Offset: 0x0001E938
		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0002074D File Offset: 0x0001E94D
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x0002075C File Offset: 0x0001E95C
		[ReportExpressionDefaultValue]
		public ReportExpression MIMEType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00020771 File Offset: 0x0001E971
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x00020780 File Offset: 0x0001E980
		[DefaultValue(Sizings.AutoSize)]
		public Sizings Sizing
		{
			get
			{
				return (Sizings)base.PropertyStore.GetInteger(21);
			}
			set
			{
				base.PropertyStore.SetInteger(21, (int)value);
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00020790 File Offset: 0x0001E990
		public Image()
		{
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00020798 File Offset: 0x0001E998
		internal Image(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000207A4 File Offset: 0x0001E9A4
		internal override void UpdateNamedReferences(NameChanges nameChanges)
		{
			base.UpdateNamedReferences(nameChanges);
			this.Value = nameChanges.GetNewName(NameChanges.EntryType.EmbeddedImage, this.Value.Expression);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x000207D8 File Offset: 0x0001E9D8
		protected override void GetDependenciesCore(IList<ReportObject> dependencies)
		{
			base.GetDependenciesCore(dependencies);
			Image.GetEmbeddedImgDependencies(base.GetAncestor<Report>(), dependencies, this.Source, this.Value);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x000207FC File Offset: 0x0001E9FC
		internal static void GetEmbeddedImgDependencies(Report report, ICollection<ReportObject> dependencies, SourceType imageSource, ReportExpression imageValue)
		{
			if (report != null && dependencies != null && !string.IsNullOrEmpty(imageValue.Expression) && imageSource == SourceType.Embedded)
			{
				if (!imageValue.IsExpression)
				{
					EmbeddedImage embeddedImage = report.GetEmbeddedImageByName(imageValue.Expression);
					if (embeddedImage != null && !dependencies.Contains(embeddedImage))
					{
						dependencies.Add(embeddedImage);
						return;
					}
				}
				else
				{
					Expression expression = ExpressionFactory.CreateExpression(imageValue.Expression, true);
					if (expression != null)
					{
						EmbeddedImage embeddedImage;
						Expression.IterateExpressionTree(delegate(IInternalExpression expressionNode)
						{
							if (expressionNode is ConstantString)
							{
								embeddedImage = report.GetEmbeddedImageByName(expressionNode.Evaluate() as string);
								if (embeddedImage != null && !dependencies.Contains(embeddedImage))
								{
									dependencies.Add(embeddedImage);
								}
							}
						}, expression.InternalExpression);
					}
				}
			}
		}

		// Token: 0x020003A1 RID: 929
		internal new class Definition : DefinitionStore<Image, Image.Definition.Properties>
		{
			// Token: 0x06001844 RID: 6212 RVA: 0x0003B54B File Offset: 0x0003974B
			private Definition()
			{
			}

			// Token: 0x020004BA RID: 1210
			internal enum Properties
			{
				// Token: 0x04000DF9 RID: 3577
				Style,
				// Token: 0x04000DFA RID: 3578
				Name,
				// Token: 0x04000DFB RID: 3579
				ActionInfo,
				// Token: 0x04000DFC RID: 3580
				Top,
				// Token: 0x04000DFD RID: 3581
				Left,
				// Token: 0x04000DFE RID: 3582
				Height,
				// Token: 0x04000DFF RID: 3583
				Width,
				// Token: 0x04000E00 RID: 3584
				ZIndex,
				// Token: 0x04000E01 RID: 3585
				Visibility,
				// Token: 0x04000E02 RID: 3586
				ToolTip,
				// Token: 0x04000E03 RID: 3587
				ToolTipLocID,
				// Token: 0x04000E04 RID: 3588
				DocumentMapLabel,
				// Token: 0x04000E05 RID: 3589
				DocumentMapLabelLocID,
				// Token: 0x04000E06 RID: 3590
				Bookmark,
				// Token: 0x04000E07 RID: 3591
				RepeatWith,
				// Token: 0x04000E08 RID: 3592
				CustomProperties,
				// Token: 0x04000E09 RID: 3593
				DataElementName,
				// Token: 0x04000E0A RID: 3594
				DataElementOutput,
				// Token: 0x04000E0B RID: 3595
				Source,
				// Token: 0x04000E0C RID: 3596
				Value,
				// Token: 0x04000E0D RID: 3597
				MIMEType,
				// Token: 0x04000E0E RID: 3598
				Sizing
			}
		}
	}
}
