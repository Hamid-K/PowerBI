using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200010C RID: 268
	public sealed class FilterSegment : ODataPathSegment
	{
		// Token: 0x06000F49 RID: 3913 RVA: 0x00026118 File Offset: 0x00024318
		public FilterSegment(SingleValueNode expression, RangeVariable rangeVariable, IEdmNavigationSource navigationSource)
		{
			ExceptionUtils.CheckArgumentNotNull<SingleValueNode>(expression, "expression");
			ExceptionUtils.CheckArgumentNotNull<RangeVariable>(rangeVariable, "rangeVariable");
			ExceptionUtils.CheckArgumentNotNull<IEdmNavigationSource>(navigationSource, "navigationSource");
			base.Identifier = "$filter";
			base.SingleResult = false;
			base.TargetEdmNavigationSource = navigationSource;
			base.TargetKind = RequestTargetKind.Resource;
			base.TargetEdmType = rangeVariable.TypeReference.Definition;
			this.expression = expression;
			this.rangeVariable = rangeVariable;
			this.bindingType = navigationSource.Type;
			NodeToStringBuilder nodeToStringBuilder = new NodeToStringBuilder();
			string text = nodeToStringBuilder.TranslateNode(expression);
			this.literalText = "$filter(" + text + ")";
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x000261BE File Offset: 0x000243BE
		public SingleValueNode Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x000261C6 File Offset: 0x000243C6
		public RangeVariable RangeVariable
		{
			get
			{
				return this.rangeVariable;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x000261CE File Offset: 0x000243CE
		public IEdmTypeReference ItemType
		{
			get
			{
				return this.RangeVariable.TypeReference;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x000261DB File Offset: 0x000243DB
		public override IEdmType EdmType
		{
			get
			{
				return this.bindingType;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x000261E3 File Offset: 0x000243E3
		public string LiteralText
		{
			get
			{
				return this.literalText;
			}
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x000261EB File Offset: 0x000243EB
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00026200 File Offset: 0x00024400
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00026218 File Offset: 0x00024418
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			FilterSegment filterSegment = other as FilterSegment;
			return filterSegment != null && filterSegment.TargetEdmNavigationSource == base.TargetEdmNavigationSource && filterSegment.Expression == this.Expression && filterSegment.ItemType == this.ItemType && filterSegment.RangeVariable == this.RangeVariable;
		}

		// Token: 0x0400077C RID: 1916
		private readonly SingleValueNode expression;

		// Token: 0x0400077D RID: 1917
		private readonly RangeVariable rangeVariable;

		// Token: 0x0400077E RID: 1918
		private readonly IEdmType bindingType;

		// Token: 0x0400077F RID: 1919
		private readonly string literalText;
	}
}
