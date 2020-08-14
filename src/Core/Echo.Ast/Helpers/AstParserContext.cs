using System.Collections.Generic;
using Echo.ControlFlow;
using Echo.ControlFlow.Regions;
using Echo.Core.Code;
using Echo.DataFlow;

namespace Echo.Ast.Helpers
{
    internal sealed class AstParserContext<TInstruction>
    {
        internal readonly ControlFlowGraph<TInstruction> ControlFlowGraph;
        internal readonly DataFlowGraph<TInstruction> DataFlowGraph;
        internal readonly IInstructionSetArchitecture<StatementBase<TInstruction>> AstArchitecture;
        internal readonly Dictionary<IVariable, int> VariableVersions = new Dictionary<IVariable, int>();
        internal readonly Dictionary<(IVariable, int), AstVariable> VersionedAstVariables = new Dictionary<(IVariable, int), AstVariable>();
        internal readonly Dictionary<long, Dictionary<IVariable, int>> InstructionVersionStates = new Dictionary<long, Dictionary<IVariable, int>>();
        internal readonly Dictionary<AstVariableCollection, AstVariable> PhiSlots = new Dictionary<AstVariableCollection, AstVariable>();
        internal readonly Dictionary<long, AstVariable[]> StackSlots = new Dictionary<long, AstVariable[]>();
        internal readonly Dictionary<BasicControlFlowRegion<TInstruction>, BasicControlFlowRegion<StatementBase<TInstruction>>>
            RegionsMapping = new Dictionary<BasicControlFlowRegion<TInstruction>, BasicControlFlowRegion<StatementBase<TInstruction>>>();

        internal long Id = -1;
        internal long VarCount;
        internal long PhiVarCount;

        internal AstParserContext(
            ControlFlowGraph<TInstruction> controlFlowGraph,
            DataFlowGraph<TInstruction> dataFlowGraph,
            IInstructionSetArchitecture<StatementBase<TInstruction>> astArchitecture)
        {
            ControlFlowGraph = controlFlowGraph;
            DataFlowGraph = dataFlowGraph;
            AstArchitecture = astArchitecture;
        }

        internal IInstructionSetArchitecture<TInstruction> Architecture => ControlFlowGraph.Architecture;

        internal int GetOrCreateVersion(IVariable variable)
        {
            if (!VariableVersions.ContainsKey(variable))
                VariableVersions.Add(variable, 0);

            return VariableVersions[variable];
        }

        internal int IncrementVersion(IVariable variable)
        {
            GetOrCreateVersion(variable);
            return ++VariableVersions[variable];
        }
    }
}