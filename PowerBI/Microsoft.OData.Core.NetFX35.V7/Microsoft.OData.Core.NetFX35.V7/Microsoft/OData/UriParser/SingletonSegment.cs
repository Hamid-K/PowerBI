using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000161 RID: 353
	public sealed class SingletonSegment : ODataPathSegment
	{
		// Token: 0x06000F27 RID: 3879 RVA: 0x0002B87A File Offset: 0x00029A7A
		public SingletonSegment(IEdmSingleton singleton)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmSingleton>(singleton, "singleton");
			this.singleton = singleton;
			base.TargetEdmNavigationSource = singleton;
			base.TargetEdmType = singleton.EntityType();
			base.TargetKind = RequestTargetKind.Resource;
			base.SingleResult = true;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0002B8B6 File Offset: 0x00029AB6
		public IEdmSingleton Singleton
		{
			get
			{
				return this.singleton;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0002B8BE File Offset: 0x00029ABE
		public override IEdmType EdmType
		{
			get
			{
				return this.singleton.EntityType();
			}
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0002B8CB File Offset: 0x00029ACB
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0002B8E0 File Offset: 0x00029AE0
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0002B8F8 File Offset: 0x00029AF8
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			SingletonSegment singletonSegment = other as SingletonSegment;
			return singletonSegment != null && singletonSegment.singleton == this.Singleton;
		}

		// Token: 0x040007A6 RID: 1958
		private readonly IEdmSingleton singleton;
	}
}
