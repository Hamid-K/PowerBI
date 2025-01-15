using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.InfoNav.Utils;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000138 RID: 312
	internal class DaxInvalidExternalContentException : DaxTranslationException
	{
		// Token: 0x06001134 RID: 4404 RVA: 0x0003018E File Offset: 0x0002E38E
		internal DaxInvalidExternalContentException(string message, DaxInvalidExternalContentErrorCode contentErrorCode, int errorLine, int errorPosition, ScrubbedEntityPropertyReference itemReference, DaxInvalidExternalContentType contentType)
			: base(message, CommandTreeTranslationErrorCode.InvalidDaxExternalContent)
		{
			this._contentErrorCode = contentErrorCode;
			this._errorLine = errorLine;
			this._errorPosition = errorPosition;
			this._itemReference = itemReference;
			this._contentType = contentType;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000301C0 File Offset: 0x0002E3C0
		protected DaxInvalidExternalContentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._contentErrorCode = (DaxInvalidExternalContentErrorCode)info.GetValue("ContentErrorCode", typeof(DaxInvalidExternalContentErrorCode));
			this._errorLine = (int)info.GetValue("ErrorLine", typeof(int));
			this._errorPosition = (int)info.GetValue("ErrorPosition", typeof(int));
			this._itemReference = (ScrubbedEntityPropertyReference)info.GetValue("ItemReference", typeof(ScrubbedEntityPropertyReference));
			this._contentType = (DaxInvalidExternalContentType)info.GetValue("ContentType", typeof(DaxInvalidExternalContentType));
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00030278 File Offset: 0x0002E478
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ContentErrorCode", this.ContentErrorCode);
			info.AddValue("ErrorLine", this.ErrorLine);
			info.AddValue("ErrorPosition", this.ErrorPosition);
			info.AddValue("ItemReference", this.ItemReference);
			info.AddValue("ContentType", this.ContentType);
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x000302EC File Offset: 0x0002E4EC
		public DaxInvalidExternalContentErrorCode ContentErrorCode
		{
			get
			{
				return this._contentErrorCode;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x000302F4 File Offset: 0x0002E4F4
		public int ErrorLine
		{
			get
			{
				return this._errorLine;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x000302FC File Offset: 0x0002E4FC
		public int ErrorPosition
		{
			get
			{
				return this._errorPosition;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x00030304 File Offset: 0x0002E504
		public ScrubbedEntityPropertyReference ItemReference
		{
			get
			{
				return this._itemReference;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x0003030C File Offset: 0x0002E50C
		public DaxInvalidExternalContentType ContentType
		{
			get
			{
				return this._contentType;
			}
		}

		// Token: 0x04000AB7 RID: 2743
		private const string ContentErrorCodeSlotName = "ContentErrorCode";

		// Token: 0x04000AB8 RID: 2744
		private const string ErrorLineSlotName = "ErrorLine";

		// Token: 0x04000AB9 RID: 2745
		private const string ErrorPositionSlotName = "ErrorPosition";

		// Token: 0x04000ABA RID: 2746
		private const string ItemReferenceSlotName = "ItemReference";

		// Token: 0x04000ABB RID: 2747
		private const string ContentTypeSlotName = "ContentType";

		// Token: 0x04000ABC RID: 2748
		private readonly DaxInvalidExternalContentErrorCode _contentErrorCode;

		// Token: 0x04000ABD RID: 2749
		private readonly int _errorLine;

		// Token: 0x04000ABE RID: 2750
		private readonly int _errorPosition;

		// Token: 0x04000ABF RID: 2751
		private readonly ScrubbedEntityPropertyReference _itemReference;

		// Token: 0x04000AC0 RID: 2752
		private readonly DaxInvalidExternalContentType _contentType;
	}
}
