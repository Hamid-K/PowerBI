using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel.ExpressionParser;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001F7 RID: 503
	public class BackgroundImage : ReportObject
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x00027610 File Offset: 0x00025810
		// (set) Token: 0x060010E1 RID: 4321 RVA: 0x0002761E File Offset: 0x0002581E
		public SourceType Source
		{
			get
			{
				return (SourceType)base.PropertyStore.GetInteger(0);
			}
			set
			{
				base.PropertyStore.SetInteger(0, (int)value);
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0002762D File Offset: 0x0002582D
		// (set) Token: 0x060010E3 RID: 4323 RVA: 0x0002763B File Offset: 0x0002583B
		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x0002764F File Offset: 0x0002584F
		// (set) Token: 0x060010E5 RID: 4325 RVA: 0x0002765D File Offset: 0x0002585D
		[ReportExpressionDefaultValue]
		public ReportExpression MIMEType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x00027671 File Offset: 0x00025871
		// (set) Token: 0x060010E7 RID: 4327 RVA: 0x0002767F File Offset: 0x0002587F
		[ReportExpressionDefaultValue(typeof(BackgroundRepeatTypes), BackgroundRepeatTypes.Default)]
		public ReportExpression<BackgroundRepeatTypes> BackgroundRepeat
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<BackgroundRepeatTypes>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x00027693 File Offset: 0x00025893
		// (set) Token: 0x060010E9 RID: 4329 RVA: 0x000276A1 File Offset: 0x000258A1
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> TransparentColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x000276B5 File Offset: 0x000258B5
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x000276C3 File Offset: 0x000258C3
		[ReportExpressionDefaultValue(typeof(BackgroundPositions), BackgroundPositions.Default)]
		public ReportExpression<BackgroundPositions> Position
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<BackgroundPositions>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x000276D7 File Offset: 0x000258D7
		public BackgroundImage()
		{
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x000276DF File Offset: 0x000258DF
		internal BackgroundImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000276E8 File Offset: 0x000258E8
		protected override void GetDependenciesCore(IList<ReportObject> dependencies)
		{
			base.GetDependenciesCore(dependencies);
			if (this.Source == SourceType.Embedded)
			{
				Report ancestor = base.GetAncestor<Report>();
				if (ancestor != null)
				{
					if (!this.Value.IsExpression)
					{
						EmbeddedImage embeddedImage = ancestor.GetEmbeddedImageByName(this.Value.Expression);
						if (embeddedImage != null && !dependencies.Contains(embeddedImage))
						{
							dependencies.Add(embeddedImage);
							return;
						}
					}
					else
					{
						Expression expression = ExpressionFactory.CreateExpression(this.Value.Expression, true);
						if (expression == null)
						{
							return;
						}
						if (expression.ObjectDependencyList != null && expression.ObjectDependencyList.Count > 0)
						{
							foreach (IInternalExpression internalExpression in expression.ObjectDependencyList)
							{
								if (internalExpression is ConstantString)
								{
									EmbeddedImage embeddedImage = ancestor.GetEmbeddedImageByName((string)internalExpression.Evaluate());
									if (embeddedImage != null && !dependencies.Contains(embeddedImage))
									{
										dependencies.Add(embeddedImage);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x020003FC RID: 1020
		internal class Definition : DefinitionStore<BackgroundImage, BackgroundImage.Definition.Properties>
		{
			// Token: 0x060018C5 RID: 6341 RVA: 0x0003BC3C File Offset: 0x00039E3C
			private Definition()
			{
			}

			// Token: 0x0200050D RID: 1293
			internal enum Properties
			{
				// Token: 0x040010E3 RID: 4323
				Source,
				// Token: 0x040010E4 RID: 4324
				Value,
				// Token: 0x040010E5 RID: 4325
				MIMEType,
				// Token: 0x040010E6 RID: 4326
				BackgroundRepeat,
				// Token: 0x040010E7 RID: 4327
				TransparentColor,
				// Token: 0x040010E8 RID: 4328
				Position,
				// Token: 0x040010E9 RID: 4329
				PropertyCount
			}
		}
	}
}
