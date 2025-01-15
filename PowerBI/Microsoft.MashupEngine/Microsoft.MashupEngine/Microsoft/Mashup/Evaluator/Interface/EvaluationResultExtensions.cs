using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DCE RID: 7630
	public static class EvaluationResultExtensions
	{
		// Token: 0x0600BD03 RID: 48387 RVA: 0x00265DA8 File Offset: 0x00263FA8
		public static void InvokeThenOnDispose<T>(this Action<EvaluationResult2<T>> callback, EvaluationResult2<T> result, Action action)
		{
			if (typeof(T) == typeof(IDataReaderSource))
			{
				((Action<EvaluationResult2<IDataReaderSource>>)callback).InvokeThenOnDispose((EvaluationResult2<IDataReaderSource>)result, action);
				return;
			}
			if (typeof(T) == typeof(IPreviewValueSource))
			{
				((Action<EvaluationResult2<IPreviewValueSource>>)callback).InvokeThenOnDispose((EvaluationResult2<IPreviewValueSource>)result, action);
				return;
			}
			if (typeof(T) == typeof(IStreamSource))
			{
				((Action<EvaluationResult2<IStreamSource>>)callback).InvokeThenOnDispose((EvaluationResult2<IStreamSource>)result, action);
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600BD04 RID: 48388 RVA: 0x00265E54 File Offset: 0x00264054
		public static void InvokeThenOnDispose(this Action<EvaluationResult2<IDataReaderSource>> callback, EvaluationResult2<IDataReaderSource> result, Action action)
		{
			if (result.Exception != null)
			{
				try
				{
					callback(result);
					return;
				}
				finally
				{
					action();
				}
			}
			callback(new EvaluationResult2<IDataReaderSource>(new NotifyingDataReaderSource(result.Result, action)));
		}

		// Token: 0x0600BD05 RID: 48389 RVA: 0x00265EA4 File Offset: 0x002640A4
		public static void InvokeThenOnDispose(this Action<EvaluationResult2<IPreviewValueSource>> callback, EvaluationResult2<IPreviewValueSource> result, Action action)
		{
			if (result.Exception != null)
			{
				try
				{
					callback(result);
					return;
				}
				finally
				{
					action();
				}
			}
			callback(new EvaluationResult2<IPreviewValueSource>(new NotifyingPreviewValueSource(result.Result, action)));
		}

		// Token: 0x0600BD06 RID: 48390 RVA: 0x00265EF4 File Offset: 0x002640F4
		public static void InvokeThenOnDispose(this Action<EvaluationResult2<IStreamSource>> callback, EvaluationResult2<IStreamSource> result, Action action)
		{
			if (result.Exception != null)
			{
				try
				{
					callback(result);
					return;
				}
				finally
				{
					action();
				}
			}
			callback(new EvaluationResult2<IStreamSource>(new NotifyingStreamSource(result.Result, action)));
		}

		// Token: 0x0600BD07 RID: 48391 RVA: 0x00265F44 File Offset: 0x00264144
		public static void Dispose<T>(this EvaluationResult2<T> result)
		{
			if (result.Exception == null && result.Result != null)
			{
				IDisposable disposable = result.Result as IDisposable;
				if (disposable == null)
				{
					throw new NotSupportedException();
				}
				disposable.Dispose();
			}
		}
	}
}
