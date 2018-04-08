using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Interfaces;
using Felix.Library.Data;

namespace Felix.Library.BLL
{
    public abstract class WBUpdateStrategy
    {
        public WaveBuilderItem Item { get; set; }

        protected IWaveIdProvider IdProvider {get; set;}

        protected int ParentId { get; set; }

        public WBUpdateStrategy(IWaveIdProvider idProvider, int parentId, WaveBuilderItem item)
        {
            Item = item;

            IdProvider = idProvider;

            ParentId = parentId;
        }

        public abstract WaveBuilderTrendStatus Execute(IMove move);
    }

    public class WBS0UpdateStrategy : WBUpdateStrategy
    {
        public WBS0UpdateStrategy (IWaveIdProvider idProvider, int parentId, WaveBuilderItem item): base(idProvider, parentId, item){}

        public override WaveBuilderTrendStatus Execute(IMove move)
        {
            IWave childWave = Wave.Open(IdProvider.CurrentId, ParentId, move);

            Item.Wave.AddChild(childWave);

            Item.State = WaveBuilderState.S1;

            return WaveBuilderTrendStatus.Continue;
        }
    }

    public class WBS1UpdateStrategy : WBUpdateStrategy
    {
        public WBS1UpdateStrategy(IWaveIdProvider idProvider, int parentId, WaveBuilderItem item) : base(idProvider, parentId, item) { }

        public override WaveBuilderTrendStatus Execute(IMove move)
        {
            IWave childWave = Wave.Open(IdProvider.CurrentId, ParentId, move);

            Item.Wave.AddChild(childWave);

            Item.State = WaveBuilderState.S1;

            return WaveBuilderTrendStatus.Continue;
        }
    }

    public class WBS2UpdateStrategy : WBUpdateStrategy
    {
        public WBS2UpdateStrategy(IWaveIdProvider idProvider, int parentId, WaveBuilderItem item) : base(idProvider, parentId, item) { }

        public override WaveBuilderTrendStatus Execute(IMove move)
        {
            IWave childWave = Wave.Open(IdProvider.CurrentId, ParentId, move);

            Item.Wave.AddChild(childWave);

            Item.State = WaveBuilderState.S1;

            return WaveBuilderTrendStatus.Continue;
        }
    }

    public class WBS3UpdateStrategy : WBUpdateStrategy
    {
        public WBS3UpdateStrategy(IWaveIdProvider idProvider, int parentId, WaveBuilderItem item) : base(idProvider, parentId, item) { }

        public override WaveBuilderTrendStatus Execute(IMove move)
        {
            IWave childWave = Wave.Open(IdProvider.CurrentId, ParentId, move);

            Item.Wave.AddChild(childWave);

            Item.State = WaveBuilderState.S1;

            return WaveBuilderTrendStatus.Continue;
        }
    }

    public class WBS4UpdateStrategy : WBUpdateStrategy
    {
        public WBS4UpdateStrategy(IWaveIdProvider idProvider, int parentId, WaveBuilderItem item) : base(idProvider, parentId, item) { }

        public override WaveBuilderTrendStatus Execute(IMove move)
        {
            IWave childWave = Wave.Open(IdProvider.CurrentId, ParentId, move);

            Item.Wave.AddChild(childWave);

            Item.State = WaveBuilderState.S1;

            return WaveBuilderTrendStatus.Continue;
        }
    }

    public class WBS5UpdateStrategy : WBUpdateStrategy
    {
        public WBS5UpdateStrategy(IWaveIdProvider idProvider, int parentId, WaveBuilderItem item) : base(idProvider, parentId, item) { }

        public override WaveBuilderTrendStatus Execute(IMove move)
        {
            IWave childWave = Wave.Open(IdProvider.CurrentId, ParentId, move);

            Item.Wave.AddChild(childWave);

            Item.State = WaveBuilderState.S1;

            return WaveBuilderTrendStatus.Continue;
        }
    }
}
