using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200013B RID: 315
	public sealed class KeySegment : ODataPathSegment
	{
		// Token: 0x06000E29 RID: 3625 RVA: 0x00029610 File Offset: 0x00027810
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Using key value pair is exactly what we want here.")]
		public KeySegment(IEnumerable<KeyValuePair<string, object>> keys, IEdmEntityType edmType, IEdmNavigationSource navigationSource)
		{
			this.keys = new ReadOnlyCollection<KeyValuePair<string, object>>(Enumerable.ToList<KeyValuePair<string, object>>(keys));
			this.edmType = edmType;
			this.navigationSource = navigationSource;
			base.SingleResult = true;
			if (navigationSource != null)
			{
				ExceptionUtil.ThrowIfTypesUnrelated(edmType, navigationSource.EntityType(), "KeySegments");
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0002965D File Offset: 0x0002785D
		public KeySegment(ODataPathSegment previous, IEnumerable<KeyValuePair<string, object>> keys, IEdmEntityType edmType, IEdmNavigationSource navigationSource)
			: this(keys, edmType, navigationSource)
		{
			if (previous != null)
			{
				base.CopyValuesFrom(previous);
				base.SingleResult = true;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0002967A File Offset: 0x0002787A
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Using key value pair is exactly what we want here.")]
		public IEnumerable<KeyValuePair<string, object>> Keys
		{
			get
			{
				return Enumerable.AsEnumerable<KeyValuePair<string, object>>(this.keys);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00029687 File Offset: 0x00027887
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0002968F File Offset: 0x0002788F
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00029697 File Offset: 0x00027897
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x000296AC File Offset: 0x000278AC
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x000296C4 File Offset: 0x000278C4
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			KeySegment keySegment = other as KeySegment;
			return keySegment != null && Enumerable.SequenceEqual<KeyValuePair<string, object>>(keySegment.Keys, this.Keys) && keySegment.EdmType == this.edmType && keySegment.NavigationSource == this.navigationSource;
		}

		// Token: 0x04000762 RID: 1890
		private readonly ReadOnlyCollection<KeyValuePair<string, object>> keys;

		// Token: 0x04000763 RID: 1891
		private readonly IEdmEntityType edmType;

		// Token: 0x04000764 RID: 1892
		private readonly IEdmNavigationSource navigationSource;
	}
}
