using System.Collections.Generic;
using System.Linq;
using Stryker.Abstractions.Mutants;
using Stryker.Abstractions.Mutators;
using Stryker.Abstractions;
using Stryker.Abstractions.ProjectComponents;

namespace Stryker.Abstractions.MutantFilters
{
    public class IgnoreBlockMutantFilter : IMutantFilter
    {
        private readonly HashSet<MutantStatus> _inactiveStatuses;

        public string DisplayName => "block already covered filter";

        public MutantFilter Type => MutantFilter.IgnoreBlockRemoval;

        public IgnoreBlockMutantFilter()
        {
            _inactiveStatuses = new HashSet<MutantStatus>
            {
                MutantStatus.Ignored,
                MutantStatus.CompileError,
            };
        }

        public IEnumerable<Mutant> FilterMutants(IEnumerable<Mutant> mutants, IReadOnlyFileLeaf file, StrykerOptions options)
        {
            var blockMutants = mutants.Where(m => m.Mutation.Type == Mutator.Block);
            var mutantsToIgnore = blockMutants.Where(blockMutant => mutants.Any(
                m => m != blockMutant
                && !_inactiveStatuses.Contains(m.ResultStatus)
                && blockMutant.Mutation.OriginalNode.Span.Contains(m.Mutation.OriginalNode.Span)
            ));

            return mutants.Except(mutantsToIgnore);
        }
    }
}
