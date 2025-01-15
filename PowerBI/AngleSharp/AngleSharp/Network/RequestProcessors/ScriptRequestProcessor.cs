using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Services;
using AngleSharp.Services.Scripting;

namespace AngleSharp.Network.RequestProcessors
{
	// Token: 0x020000A7 RID: 167
	internal sealed class ScriptRequestProcessor : IRequestProcessor
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x0001EFF8 File Offset: 0x0001D1F8
		private ScriptRequestProcessor(HtmlScriptElement script, Document document, IResourceLoader loader)
		{
			this._script = script;
			this._document = document;
			this._loader = loader;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001F018 File Offset: 0x0001D218
		internal static ScriptRequestProcessor Create(HtmlScriptElement script)
		{
			Document owner = script.Owner;
			IResourceLoader loader = owner.Loader;
			return new ScriptRequestProcessor(script, owner, loader);
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0001F03B File Offset: 0x0001D23B
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0001F043 File Offset: 0x0001D243
		public IDownload Download { get; private set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001F04C File Offset: 0x0001D24C
		public IScriptEngine Engine
		{
			get
			{
				IScriptEngine scriptEngine;
				if ((scriptEngine = this._engine) == null)
				{
					scriptEngine = (this._engine = this._document.Options.GetScriptEngine(this.ScriptLanguage));
				}
				return scriptEngine;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001F084 File Offset: 0x0001D284
		public string AlternativeLanguage
		{
			get
			{
				string ownAttribute = this._script.GetOwnAttribute(AttributeNames.Language);
				if (ownAttribute == null)
				{
					return null;
				}
				return "text/" + ownAttribute;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001F0B4 File Offset: 0x0001D2B4
		public string ScriptLanguage
		{
			get
			{
				string text = this._script.Type ?? this.AlternativeLanguage;
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				return MimeTypeNames.DefaultJavaScript;
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001F0E8 File Offset: 0x0001D2E8
		public async Task RunAsync(CancellationToken cancel)
		{
			IDownload download = this.Download;
			if (download != null)
			{
				try
				{
					ScriptRequestProcessor scriptRequestProcessor = this;
					IResponse response = scriptRequestProcessor._response;
					IResponse response2 = await download.Task.ConfigureAwait(false);
					scriptRequestProcessor._response = response2;
					scriptRequestProcessor = null;
				}
				catch (Exception)
				{
					this.FireErrorEvent();
				}
			}
			if (this._response != null && !this._script.FireSimpleEvent(EventNames.BeforeScriptExecute, false, true))
			{
				ScriptOptions scriptOptions = this.CreateOptions();
				int insert = this._document.Source.Index;
				try
				{
					await this._engine.EvaluateScriptAsync(this._response, scriptOptions, cancel).ConfigureAwait(false);
				}
				catch
				{
				}
				this._document.Source.Index = insert;
				this.FireAfterScriptExecuteEvent();
				this._document.QueueTask(new Action(this.FireLoadEvent));
				this._response.Dispose();
				this._response = null;
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001F138 File Offset: 0x0001D338
		public void Process(string content)
		{
			if (this.Engine != null)
			{
				this._response = VirtualResponse.Create(delegate(VirtualResponse res)
				{
					res.Content(content).Address(this._script.BaseUri);
				});
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001F178 File Offset: 0x0001D378
		public Task ProcessAsync(ResourceRequest request)
		{
			if (this._loader != null && this.Engine != null)
			{
				this.Download = this._loader.FetchWithCors(new CorsRequest(request)
				{
					Behavior = OriginBehavior.Taint,
					Setting = this._script.CrossOrigin.ToEnum(CorsSetting.None),
					Integrity = this._document.Options.GetProvider<IIntegrityProvider>()
				});
				return this.Download.Task;
			}
			return null;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001F1ED File Offset: 0x0001D3ED
		private ScriptOptions CreateOptions()
		{
			return new ScriptOptions(this._document)
			{
				Element = this._script,
				Encoding = TextEncoding.Resolve(this._script.CharacterSet)
			};
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001F21C File Offset: 0x0001D41C
		private void FireLoadEvent()
		{
			this._script.FireSimpleEvent(EventNames.Load, false, false);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001F231 File Offset: 0x0001D431
		private void FireErrorEvent()
		{
			this._script.FireSimpleEvent(EventNames.Error, false, false);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001F246 File Offset: 0x0001D446
		private void FireAfterScriptExecuteEvent()
		{
			this._script.FireSimpleEvent(EventNames.AfterScriptExecute, true, false);
		}

		// Token: 0x040003CB RID: 971
		private readonly HtmlScriptElement _script;

		// Token: 0x040003CC RID: 972
		private readonly Document _document;

		// Token: 0x040003CD RID: 973
		private readonly IResourceLoader _loader;

		// Token: 0x040003CE RID: 974
		private IResponse _response;

		// Token: 0x040003CF RID: 975
		private IScriptEngine _engine;
	}
}
