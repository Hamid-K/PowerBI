using System;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;

namespace System.Web.Http.Validation
{
	// Token: 0x02000090 RID: 144
	public class ModelStateFormatterLogger : IFormatterLogger
	{
		// Token: 0x06000381 RID: 897 RVA: 0x0000A583 File Offset: 0x00008783
		public ModelStateFormatterLogger(ModelStateDictionary modelState, string prefix)
		{
			if (modelState == null)
			{
				throw Error.ArgumentNull("modelState");
			}
			this._modelState = modelState;
			this._prefix = prefix;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000A5A8 File Offset: 0x000087A8
		public void LogError(string errorPath, string errorMessage)
		{
			if (errorPath == null)
			{
				throw Error.ArgumentNull("errorPath");
			}
			if (errorMessage == null)
			{
				throw Error.ArgumentNull("errorMessage");
			}
			string text = ModelBindingHelper.ConcatenateKeys(this._prefix, errorPath);
			this._modelState.AddModelError(text, errorMessage);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000A5EC File Offset: 0x000087EC
		public void LogError(string errorPath, Exception exception)
		{
			if (errorPath == null)
			{
				throw Error.ArgumentNull("errorPath");
			}
			if (exception == null)
			{
				throw Error.ArgumentNull("exception");
			}
			string text = ModelBindingHelper.ConcatenateKeys(this._prefix, errorPath);
			this._modelState.AddModelError(text, exception);
		}

		// Token: 0x040000C9 RID: 201
		private readonly ModelStateDictionary _modelState;

		// Token: 0x040000CA RID: 202
		private readonly string _prefix;
	}
}
