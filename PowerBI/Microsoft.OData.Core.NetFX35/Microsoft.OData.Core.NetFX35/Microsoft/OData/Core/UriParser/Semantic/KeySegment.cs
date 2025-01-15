using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000242 RID: 578
	public sealed class KeySegment : ODataPathSegment
	{
		// Token: 0x060014A8 RID: 5288 RVA: 0x00049BF1 File Offset: 0x00047DF1
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Using key value pair is exactly what we want here.")]
		[SuppressMessage("Microsoft.MSInternal", "CA908:AvoidTypesThatRequireJitCompilationInPrecompiledAssemblies", Justification = "Using key value pair is exactly what we want here.")]
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
		public KeySegment(IEnumerable<KeyValuePair<string, object>> keys, IEdmEntityType edmType, IEdmNavigationSource navigationSource)
		{
			this.keys = new ReadOnlyCollection<KeyValuePair<string, object>>(Enumerable.ToList<KeyValuePair<string, object>>(keys));
			this.edmType = edmType;
			this.navigationSource = navigationSource;
			if (navigationSource != null)
			{
				ExceptionUtil.ThrowIfTypesUnrelated(edmType, navigationSource.EntityType(), "KeySegments");
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x00049C2C File Offset: 0x00047E2C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Using key value pair is exactly what we want here.")]
		public IEnumerable<KeyValuePair<string, object>> Keys
		{
			[SuppressMessage("Microsoft.MSInternal", "CA908:AvoidTypesThatRequireJitCompilationInPrecompiledAssemblies", Justification = "Using key value pair is exactly what we want here.")]
			get
			{
				return Enumerable.AsEnumerable<KeyValuePair<string, object>>(this.keys);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x00049C39 File Offset: 0x00047E39
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x00049C41 File Offset: 0x00047E41
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00049C49 File Offset: 0x00047E49
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00049C5D File Offset: 0x00047E5D
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00049C74 File Offset: 0x00047E74
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			KeySegment keySegment = other as KeySegment;
			return keySegment != null && Enumerable.SequenceEqual<KeyValuePair<string, object>>(keySegment.Keys, this.Keys) && keySegment.EdmType == this.edmType && keySegment.NavigationSource == this.navigationSource;
		}

		// Token: 0x040008AD RID: 2221
		[SuppressMessage("Microsoft.MSInternal", "CA908:AvoidTypesThatRequireJitCompilationInPrecompiledAssemblies", Justification = "Using key value pair is exactly what we want here.")]
		private readonly ReadOnlyCollection<KeyValuePair<string, object>> keys;

		// Token: 0x040008AE RID: 2222
		private readonly IEdmEntityType edmType;

		// Token: 0x040008AF RID: 2223
		private readonly IEdmNavigationSource navigationSource;
	}
}
