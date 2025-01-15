using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016DE RID: 5854
	internal class LibraryBinarySource
	{
		// Token: 0x04004F23 RID: 20259
		public const string Source = "// Copyright (c) Microsoft Corporation.  All rights reserved.\r\n\r\nsection Binary;\r\n\r\nHandlers.FromBinary = (binary as nullable binary) as record =>\r\n[\r\n    GetExpression = () => TableModule!Value.Expression(binary),\r\n    GetLength = () => Binary.Length(binary),\r\n    GetStream = () => binary,\r\n\r\n    OnRange = (offset, count) => Binary.Range(binary, offset, count),\r\n    OnEnd = () => Binary.End(binary),\r\n    OnInvoke = (function, arguments, index) => Function.Invoke(function, List.ReplaceRange(arguments, index, 1, {binary})),\r\n\r\n    OnReplace = (value) => ValueAction.Replace(binary, value)\r\n];\r\n\r\nshared Binary.View = (\r\n    binary as nullable binary,\r\n    handlers as record\r\n) as binary =>\r\n    let\r\n        defaultHandlers = if (binary <> null) then Handlers.FromBinary(binary) else [],\r\n\r\n        // NOTE: Do not automatically forward Value.Expression to the binary as it breaks encapsulation\r\n        defaultHandlersWithoutExpression =\r\n            if (defaultHandlers[GetExpression]? <> null) then defaultHandlers & [GetExpression = () => null]\r\n            else defaultHandlers,\r\n\r\n        viewHandlers = defaultHandlersWithoutExpression & handlers,\r\n        view = LibraryModule!Binary.FromHandlers(viewHandlers)\r\n    in\r\n        view;\r\n\r\nshared Binary.ViewFunction = Value.ViewFunction;\r\n\r\nshared Binary.ViewError = Value.ViewError;\r\n\r\n    ";
	}
}
