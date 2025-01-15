using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Routing;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Portal.Interfaces.Repositories;
using Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.Reflection;
using Model;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.V2.Controllers
{
	// Token: 0x02000014 RID: 20
	public class ExtensionsController : EntitySetReflectionODataController<Extension>
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00005014 File Offset: 0x00003214
		public ExtensionsController(ISystemService systemService, ILogger logger)
			: base(logger)
		{
			if (systemService == null)
			{
				throw new ArgumentNullException("systemService");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._systemService = systemService;
			this._logger = logger;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005047 File Offset: 0x00003247
		public static void RegisterModel(ODataConventionModelBuilder builder)
		{
			builder.EntitySet<Extension>("Extensions");
			ActionConfiguration actionConfiguration = builder.EntityType<Extension>().Collection.Action("ValidateExtensionSettings").ReturnsCollection<ExtensionParameter>();
			actionConfiguration.CollectionParameter<ParameterValue>("ParameterValues");
			actionConfiguration.Parameter<string>("ExtensionName");
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005088 File Offset: 0x00003288
		protected override IQueryable<Extension> GetEntitySet(string castName)
		{
			Extension[] array;
			try
			{
				array = this._systemService.ListExtensions(base.User, ExtensionType.All);
			}
			catch (AccessDeniedException)
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			return array.AsQueryable<Extension>();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000050CC File Offset: 0x000032CC
		protected override Extension GetEntity(string key, string castName)
		{
			throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000050CC File Offset: 0x000032CC
		protected override bool AddEntity(Extension entity)
		{
			throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000050CC File Offset: 0x000032CC
		protected override bool AddEntity(Extension entity, out Extension createdEntity)
		{
			throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000050CC File Offset: 0x000032CC
		protected override bool PutEntity(string key, Extension entity)
		{
			throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000050CC File Offset: 0x000032CC
		protected override bool PatchEntity(string key, Extension entity, string[] delta)
		{
			throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000050CC File Offset: 0x000032CC
		protected override bool DeleteEntity(string key)
		{
			throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000050D8 File Offset: 0x000032D8
		private string ParseKey(string Id)
		{
			if (Id.StartsWith("'"))
			{
				return Id.Substring(1, Id.Length - 2);
			}
			return Id;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000050F8 File Offset: 0x000032F8
		[HttpGet]
		[ODataRoute("Extensions({Id})/Parameters")]
		public IHttpActionResult GetExtensionParameters(string Id)
		{
			string text = this.ParseKey(Id);
			ExtensionParameter[] array = null;
			try
			{
				array = this._systemService.ListExtensionParameters(base.User, text);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("InvalidParameterException"))
				{
					throw new HttpResponseException(HttpStatusCode.BadRequest);
				}
				throw ex;
			}
			return base.CreateOk(array);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000038EE File Offset: 0x00001AEE
		[HttpPut]
		[ODataRoute("Extensions({Id})/Parameters")]
		public IHttpActionResult PutExtensionParameters(string Id)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000038EE File Offset: 0x00001AEE
		[HttpPatch]
		[ODataRoute("Extensions({Id})/Parameters")]
		public IHttpActionResult PatchExtensionParameters(string Id)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000038EE File Offset: 0x00001AEE
		[HttpPost]
		[ODataRoute("Extensions({Id})/Parameters")]
		public IHttpActionResult PostExtensionParameters(string Id)
		{
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000515C File Offset: 0x0000335C
		[HttpPost]
		public virtual IHttpActionResult ValidateExtensionSettings(ODataActionParameters actionParameters)
		{
			if (!base.ModelState.IsValid)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			if (actionParameters == null)
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			if (!actionParameters.ContainsKey("ParameterValues") || !actionParameters.ContainsKey("ExtensionName"))
			{
				return base.BadRequest(base.GetModelStateValidationErrors());
			}
			string text = (string)actionParameters["ExtensionName"];
			IEnumerable<ParameterValue> enumerable = (IEnumerable<ParameterValue>)actionParameters["ParameterValues"];
			ExtensionParameter[] array = this._systemService.ValidateExtensionSettings(base.User, text, enumerable);
			return base.CreateOk(array);
		}

		// Token: 0x04000055 RID: 85
		private readonly ISystemService _systemService;

		// Token: 0x04000056 RID: 86
		private readonly ILogger _logger;
	}
}
