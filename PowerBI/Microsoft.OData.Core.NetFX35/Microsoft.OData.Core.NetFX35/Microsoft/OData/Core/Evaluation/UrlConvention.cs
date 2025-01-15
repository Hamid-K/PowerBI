using System;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x0200008E RID: 142
	internal sealed class UrlConvention
	{
		// Token: 0x0600059F RID: 1439 RVA: 0x00014A2C File Offset: 0x00012C2C
		private UrlConvention(bool generateKeyAsSegment, bool odataSimplified = false)
		{
			this.generateKeyAsSegment = generateKeyAsSegment;
			this.odataSimplified = odataSimplified;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00014A42 File Offset: 0x00012C42
		internal bool GenerateKeyAsSegment
		{
			get
			{
				return this.generateKeyAsSegment;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00014A4A File Offset: 0x00012C4A
		internal bool ODataSimplified
		{
			get
			{
				return this.odataSimplified;
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00014A52 File Offset: 0x00012C52
		internal static UrlConvention CreateWithExplicitValue(bool generateKeyAsSegment)
		{
			return new UrlConvention(generateKeyAsSegment, false);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00014A5B File Offset: 0x00012C5B
		internal static UrlConvention CreateODataSimplifiedConvention()
		{
			return new UrlConvention(false, true);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00014A64 File Offset: 0x00012C64
		internal static UrlConvention ForModel(IEdmModel model)
		{
			IEdmEntityContainer entityContainer = model.EntityContainer;
			return UrlConvention.CreateWithExplicitValue(Enumerable.Any<IEdmValueAnnotation>(Enumerable.OfType<IEdmValueAnnotation>(model.FindVocabularyAnnotations(entityContainer)), new Func<IEdmValueAnnotation, bool>(UrlConvention.IsKeyAsSegmentUrlConventionAnnotation)));
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00014A9A File Offset: 0x00012C9A
		internal static UrlConvention ForUserSettingAndTypeContext(bool? keyAsSegment, IODataFeedAndEntryTypeContext typeContext)
		{
			if (keyAsSegment != null)
			{
				return UrlConvention.CreateWithExplicitValue(keyAsSegment.Value);
			}
			return typeContext.UrlConvention;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00014AB8 File Offset: 0x00012CB8
		private static bool IsKeyAsSegmentUrlConventionAnnotation(IEdmValueAnnotation annotation)
		{
			return annotation != null && UrlConvention.IsUrlConventionTerm(annotation.Term) && UrlConvention.IsKeyAsSegment(annotation.Value);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00014AD7 File Offset: 0x00012CD7
		private static bool IsKeyAsSegment(IEdmExpression value)
		{
			return value != null && value.ExpressionKind == EdmExpressionKind.StringConstant && ((IEdmStringConstantExpression)value).Value == "KeyAsSegment";
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00014AFC File Offset: 0x00012CFC
		private static bool IsUrlConventionTerm(IEdmTerm term)
		{
			return term != null && term.Name == "UrlConventions" && term.Namespace == "Com.Microsoft.OData.Service.Conventions.V1";
		}

		// Token: 0x04000262 RID: 610
		private const string ConventionTermNamespace = "Com.Microsoft.OData.Service.Conventions.V1";

		// Token: 0x04000263 RID: 611
		private const string ConventionTermName = "UrlConventions";

		// Token: 0x04000264 RID: 612
		private const string DefaultConventionName = "Default";

		// Token: 0x04000265 RID: 613
		private const string KeyAsSegmentConventionName = "KeyAsSegment";

		// Token: 0x04000266 RID: 614
		private const string UrlConventionHeaderName = "DataServiceUrlConventions";

		// Token: 0x04000267 RID: 615
		private readonly bool generateKeyAsSegment;

		// Token: 0x04000268 RID: 616
		private readonly bool odataSimplified;
	}
}
