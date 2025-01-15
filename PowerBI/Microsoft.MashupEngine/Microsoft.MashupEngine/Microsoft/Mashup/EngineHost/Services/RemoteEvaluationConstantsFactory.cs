using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A83 RID: 6787
	internal class RemoteEvaluationConstantsFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AB2B RID: 43819 RVA: 0x00234DC4 File Offset: 0x00232FC4
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IEvaluationConstants evaluationConstants = engineHost.QueryService<IEvaluationConstants>();
			proxyInitArgs.WriteGuid(evaluationConstants.ActivityId);
			proxyInitArgs.WriteString(evaluationConstants.CorrelationId ?? string.Empty);
			proxyInitArgs.WriteList(evaluationConstants.TracedConstants.ToList<EvaluationConstant>(), delegate(BinaryWriter w, EvaluationConstant constant)
			{
				w.WriteString(constant.Name);
				w.WriteString(constant.Value);
				w.WriteBool(constant.IsPii);
			});
			return EmptyStub.Instance;
		}

		// Token: 0x0600AB2C RID: 43820 RVA: 0x00234E30 File Offset: 0x00233030
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			Guid guid = proxyInitArgs.ReadGuid();
			string text = proxyInitArgs.ReadString();
			List<EvaluationConstant> list = proxyInitArgs.ReadList((BinaryReader c) => new EvaluationConstant(c.ReadString(), c.ReadString(), c.ReadBool()));
			return new EngineHostServiceProxy(new SimpleEngineHost<IEvaluationConstants>(new EvaluationConstants(guid, text, list)));
		}
	}
}
