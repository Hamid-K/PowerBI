using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001DA9 RID: 7593
	public class ValueDeserializer
	{
		// Token: 0x0600BC3D RID: 48189 RVA: 0x002617C4 File Offset: 0x0025F9C4
		public static IValue Deserialize(IEngine engine, string text)
		{
			bool haveSourceErrors = false;
			IDocumentHost documentHost = new TextDocumentHost(text);
			ITokens tokens = engine.Tokenize(text);
			IExpressionDocument expressionDocument = engine.Parse(tokens, documentHost, delegate(IError error)
			{
				haveSourceErrors = true;
			}) as IExpressionDocument;
			if (haveSourceErrors || expressionDocument == null)
			{
				return engine.ExceptionRecord(engine.Text(Strings.Unexpected_Error), engine.Text(Strings.Evaluation_Result_Deserialization_Error), engine.Text(text));
			}
			object obj = ValueDeserializer.syncRoot;
			IRecordValue recordValue;
			lock (obj)
			{
				if (ValueDeserializer.engineHost == null)
				{
					ValueDeserializer.engineHost = new SimpleEngineHost<IResourcePermissionService>(ValueDeserializer.permissionService);
				}
				if (ValueDeserializer.library == null)
				{
					ValueDeserializer.library = engine.GetLibrary(ValueDeserializer.engineHost, null);
				}
				recordValue = ValueDeserializer.library;
				ValueDeserializer.library = null;
			}
			IValue value;
			try
			{
				value = engine.Deserialize(recordValue, expressionDocument.Expression);
			}
			finally
			{
				obj = ValueDeserializer.syncRoot;
				lock (obj)
				{
					if (ValueDeserializer.library == null)
					{
						ValueDeserializer.library = recordValue;
					}
				}
			}
			return value;
		}

		// Token: 0x04005FD3 RID: 24531
		private static readonly object syncRoot = new object();

		// Token: 0x04005FD4 RID: 24532
		private static readonly ValueDeserializer.PermissionService permissionService = new ValueDeserializer.PermissionService();

		// Token: 0x04005FD5 RID: 24533
		private static IEngineHost engineHost;

		// Token: 0x04005FD6 RID: 24534
		private static IRecordValue library;

		// Token: 0x02001DAA RID: 7594
		private class PermissionService : IResourcePermissionService
		{
			// Token: 0x0600BC40 RID: 48192 RVA: 0x0007D355 File Offset: 0x0007B555
			bool IResourcePermissionService.IsResourceAccessPermitted(IResource resource, out ResourceCredentialCollection credentials)
			{
				credentials = null;
				return false;
			}
		}
	}
}
