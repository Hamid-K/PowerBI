using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.OData.UriParser;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.SegmentHandlers;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection
{
	// Token: 0x02000042 RID: 66
	public abstract class ReflectionODataController<T> : ODataController
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000D15A File Offset: 0x0000B35A
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000D162 File Offset: 0x0000B362
		private protected ILogger Logger { protected get; private set; }

		// Token: 0x06000311 RID: 785
		protected abstract object GetRoot(Microsoft.AspNet.OData.Routing.ODataPath oDataPath, out int index);

		// Token: 0x06000312 RID: 786 RVA: 0x0000D16C File Offset: 0x0000B36C
		protected ReflectionODataController(ILogger logger)
			: this(logger, new Dictionary<string, ISegmentHandler>
			{
				{
					"cast",
					new CastSegmentHandler()
				},
				{
					"navigation",
					new PropertySegmentHandler()
				},
				{
					"property",
					new PropertySegmentHandler()
				},
				{
					"$value",
					new NoOpSegmentHandler()
				},
				{
					"$count",
					new NoOpSegmentHandler()
				}
			})
		{
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000D1D5 File Offset: 0x0000B3D5
		internal ReflectionODataController(ILogger logger, Dictionary<string, ISegmentHandler> handlers)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			if (handlers == null)
			{
				throw new ArgumentNullException("handlers");
			}
			this.Logger = logger;
			this._handlers = handlers;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000D208 File Offset: 0x0000B408
		internal string GetModelStateValidationErrors()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, ModelState> keyValuePair in base.ModelState)
			{
				foreach (ModelError modelError in keyValuePair.Value.Errors)
				{
					if (modelError.Exception != null)
					{
						list.Add(modelError.Exception.Message);
					}
				}
			}
			string combinedErrorsString = string.Join(",", list);
			this.Logger.Trace(TraceLevel.Warning, () => string.Format("BadRequest, Errors: {0}", combinedErrorsString));
			return combinedErrorsString;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
		[EnableQuery]
		public IHttpActionResult Get(Microsoft.AspNet.OData.Routing.ODataPath oDataPath)
		{
			this.Logger.Trace(TraceLevel.Verbose, () => string.Format("GET '{0}' ({1})", this.Request.RequestUri, oDataPath.PathTemplate));
			if (!base.ModelState.IsValid)
			{
				return this.BadRequest(this.GetModelStateValidationErrors());
			}
			int i = 0;
			object obj = this.GetRoot(oDataPath, out i);
			if (obj == null)
			{
				this.Logger.Trace(TraceLevel.Verbose, () => "No entity(s) found");
				return this.NotFound();
			}
			string[] array = oDataPath.PathTemplate.Split(new string[] { "~/", "/" }, StringSplitOptions.RemoveEmptyEntries);
			while (i < oDataPath.Segments.Count)
			{
				ODataPathSegment segment = oDataPath.Segments[i];
				string text = array[i];
				if (!this._handlers.ContainsKey(text))
				{
					this.Logger.Trace(TraceLevel.Verbose, () => string.Format("Unhandled segment type ({0})", segment.Identifier));
					return this.NotFound();
				}
				obj = this._handlers[text].Handle(obj, segment);
				if (obj == null)
				{
					return this.NotFound();
				}
				i++;
			}
			return this.CreateOk(obj);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000D444 File Offset: 0x0000B644
		internal BadRequestResult BadRequest()
		{
			return base.BadRequest();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000D44C File Offset: 0x0000B64C
		internal BadRequestErrorMessageResult BadRequest(string message)
		{
			return base.BadRequest(message);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000D455 File Offset: 0x0000B655
		internal IHttpActionResult Ok()
		{
			return base.Ok();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000D45D File Offset: 0x0000B65D
		internal IHttpActionResult CreateOk(object content)
		{
			return (IHttpActionResult)Activator.CreateInstance(typeof(OkNegotiatedContentResult<>).MakeGenericType(new Type[] { content.GetType() }), new object[] { content, this });
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000D495 File Offset: 0x0000B695
		internal NotFoundResult NotFound()
		{
			return base.NotFound();
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000D49D File Offset: 0x0000B69D
		internal StatusCodeResult StatusCode(HttpStatusCode statusCode)
		{
			return base.StatusCode(statusCode);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000D4A6 File Offset: 0x0000B6A6
		internal ResponseMessageResult ResponseMessage(HttpResponseMessage message)
		{
			return base.ResponseMessage(message);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000D4AF File Offset: 0x0000B6AF
		internal StatusCodeResult NotAllowed()
		{
			return base.StatusCode(HttpStatusCode.MethodNotAllowed);
		}

		// Token: 0x040000C5 RID: 197
		private readonly Dictionary<string, ISegmentHandler> _handlers;
	}
}
