using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using Microsoft.AspNet.OData.Adapters;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001AF RID: 431
	public class ODataDeserializerContext
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0003A92A File Offset: 0x00038B2A
		// (set) Token: 0x06000E4B RID: 3659 RVA: 0x0003A934 File Offset: 0x00038B34
		public HttpRequestMessage Request
		{
			get
			{
				return this._request;
			}
			set
			{
				this._request = value;
				this.InternalRequest = ((this._request != null) ? new WebApiRequestMessage(this._request) : null);
				this.InternalUrlHelper = ((this._request != null) ? new WebApiUrlHelper(HttpRequestMessageExtensions.GetUrlHelper(this._request)) : null);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0003A985 File Offset: 0x00038B85
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x0003A98D File Offset: 0x00038B8D
		public HttpRequestContext RequestContext { get; set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x0003A996 File Offset: 0x00038B96
		// (set) Token: 0x06000E4F RID: 3663 RVA: 0x0003A99E File Offset: 0x00038B9E
		public Type ResourceType { get; set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0003A9A7 File Offset: 0x00038BA7
		// (set) Token: 0x06000E51 RID: 3665 RVA: 0x0003A9AF File Offset: 0x00038BAF
		public IEdmTypeReference ResourceEdmType { get; set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0003A9B8 File Offset: 0x00038BB8
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x0003A9C0 File Offset: 0x00038BC0
		public ODataPath Path { get; set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0003A9C9 File Offset: 0x00038BC9
		// (set) Token: 0x06000E55 RID: 3669 RVA: 0x0003A9D1 File Offset: 0x00038BD1
		public IEdmModel Model { get; set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0003A9DA File Offset: 0x00038BDA
		// (set) Token: 0x06000E57 RID: 3671 RVA: 0x0003A9E2 File Offset: 0x00038BE2
		internal IWebApiRequestMessage InternalRequest { get; private set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0003A9EB File Offset: 0x00038BEB
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x0003A9F3 File Offset: 0x00038BF3
		internal IWebApiUrlHelper InternalUrlHelper { get; private set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0003A9FC File Offset: 0x00038BFC
		internal bool IsDeltaOfT
		{
			get
			{
				if (this._isDeltaOfT == null)
				{
					this._isDeltaOfT = new bool?(this.ResourceType != null && this.ResourceType.IsGenericType() && this.ResourceType.GetGenericTypeDefinition() == typeof(Delta<>));
				}
				return this._isDeltaOfT.Value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0003AA64 File Offset: 0x00038C64
		internal bool IsUntyped
		{
			get
			{
				if (this._isUntyped == null)
				{
					this._isUntyped = new bool?(TypeHelper.IsTypeAssignableFrom(typeof(IEdmObject), this.ResourceType) || typeof(ODataUntypedActionParameters) == this.ResourceType);
				}
				return this._isUntyped.Value;
			}
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0003AAC3 File Offset: 0x00038CC3
		internal IEdmTypeReference GetEdmType(Type type)
		{
			if (this.ResourceEdmType != null)
			{
				return this.ResourceEdmType;
			}
			return EdmLibHelpers.GetExpectedPayloadType(type, this.Path, this.Model);
		}

		// Token: 0x04000401 RID: 1025
		private HttpRequestMessage _request;

		// Token: 0x04000403 RID: 1027
		private bool? _isDeltaOfT;

		// Token: 0x04000404 RID: 1028
		private bool? _isUntyped;
	}
}
