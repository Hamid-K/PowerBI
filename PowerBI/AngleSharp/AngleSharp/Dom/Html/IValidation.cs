using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003F7 RID: 1015
	[DomNoInterfaceObject]
	public interface IValidation
	{
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002035 RID: 8245
		[DomName("willValidate")]
		bool WillValidate { get; }

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002036 RID: 8246
		[DomName("validity")]
		IValidityState Validity { get; }

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002037 RID: 8247
		[DomName("validationMessage")]
		string ValidationMessage { get; }

		// Token: 0x06002038 RID: 8248
		[DomName("checkValidity")]
		bool CheckValidity();

		// Token: 0x06002039 RID: 8249
		[DomName("setCustomValidity")]
		void SetCustomValidity(string error);
	}
}
