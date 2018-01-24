using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stage_Plan.Bll;

namespace Stage_Plan.Bll.TestUnitHelper.Mock
{
  public  class InstrumentsMock : IInstruments
    {
        public List<Instrument> AllInstruments
        {
            get;
        }

        public string BandName
        {
            get ;
        }

        public decimal Height
        {
            get ;
        }

    

        public decimal Width
        {
            get  ;
        }

        public string BandGenre { get; }

        public Instruments GetDemoTemplate()
        {
            var insts = new Instruments();
            insts.BandGenre = "Rock n Roll";
            insts.BandName = "Test Band";
            insts.BandWebAddress = "www.domain.co.uk";
            insts.Height = 768;
            insts.Width = 1024;
            insts.AllInstruments = new List<Instrument>();

            insts.AllInstruments.AddRange(GetPowerPoints());

            insts.AllInstruments.AddRange(GetMonitors());

            return insts;
        }

        private IEnumerable<Instrument> GetMonitors()
        {
            var result = new List<Instrument>();
            result.Add(new Instrument()
            {
                BandMemberName = "",
                Detail = "",
                Left = 50,
                Top = 400,
                IsFixedPosition = true,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Monitor.png",
                Text = "Monitor 01",
                SelectedInstrument = "Monitor"
            });

            result.Add(new Instrument()
            {
                BandMemberName = "",
                Detail = "",
                Left = 500,
                Top = 400,
                IsFixedPosition = false,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Monitor.png",
                Text = "Monitor 02",
                SelectedInstrument = "Monitor"
            });

            result.Add(new Instrument()
            {
                BandMemberName = "",
                Detail = "",
                Left = 850,
                Top = 400,
                IsFixedPosition = true,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Monitor.png",
                Text = "Monitor 03",
                SelectedInstrument = "Monitor"
            });

            return result;
        }

        private IEnumerable<Instrument> GetPowerPoints()
        {
            var result = new List<Instrument>();
            result.Add(new Instrument()
            {
                BandMemberName = "",
                Detail = "",
                Left = 890,
                Top = 60,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Power-plug.png",
                IsFixedPosition = true,
                Text = "Power point 01",
                SelectedInstrument = "PowerPoint"
            });

            result.Add(new Instrument()
            {
                BandMemberName = "",
                Detail = "",
                Left = 260,
                Top = 400,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Power-plug.png",
                IsFixedPosition = true,
                Text = "Power point 02",
                SelectedInstrument = "PowerPoint"
            });

            return result;
        }

        public Instruments GetDemoInstruments()
        {
            var insts = new Instruments();
            insts.BandGenre = "Rock n Roll";
            insts.BandName = "Test Band";
            insts.BandWebAddress = "www.domain.co.uk";
            insts.Height = 768;
            insts.Width = 1024;
            insts.AllInstruments = new List<Instrument>();


            insts.AllInstruments.Add(new Instrument()
            {
                BandMemberName = "Telis",
                Detail = "KICK%202%20mics%0ASN%202%20mics%0AH/H%201%20mic%0A3%20TOMS%203%20mics%0A2%20O/H%202%20mics",
                Left = 400,
                Top = 50,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Drums.png",
                Text = "Drums",
                SelectedInstrument = "Drums"

            });


            insts.AllInstruments.AddRange(GetLiam());
            insts.AllInstruments.AddRange(GetDave());



            insts.AllInstruments.Add(new Instrument()
            {
                BandMemberName = "Sarah",
                Detail = "SM58-Beta required",
                Left = 500,
                Top = 400,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Mic.png",
                Text = "Sarah's Vocals",
                SelectedInstrument = "Microphone"

            });

            insts.AllInstruments.Add(new Instrument()
            {
                BandMemberName = "Sarah",
                Detail = "Guitar and bass only",
                Left = 500,
                Top = 500,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Monitor.png",
                Text = "Vocal monitor",
                SelectedInstrument = "Monitor"

            });

            return insts;
        }

        private IEnumerable<Instrument> GetDave()
        {

            var result = new List<Instrument>();
            result.Add(new Instrument()
            {
                BandMemberName = "Dave",
                Detail = "",
                Left = 80,
                Top = 250,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Guitar.png",
                Text = "Electric Guitar",
                SelectedInstrument = "ElectricGuitar"

            });

            result.Add(new Instrument()
            {
                BandMemberName = "Dave",
                Detail = "",
                Left = 50,
                Top = 400,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Monitor.png",
                Text = "Dave's Guitar Monitor",
                SelectedInstrument = "Monitor"

            });


            result.Add(new Instrument()
            {
                BandMemberName = "Dave",
                Detail = "",
                Left = 100,
                Top = 100,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Speaker-04.png",
                Text = "Guitar amp",
                SelectedInstrument = "Speaker-04"

            });


            result.Add(new Instrument()
            {
                BandMemberName = "Dave",
                Detail = "",
                Left = 190,
                Top = 200,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Power-plug.png",
                Text = "Guitar Power point",
                SelectedInstrument = "PowerPoint"

            });
            return result;
        }

        private IEnumerable<Instrument> GetLiam()
        {
            var result = new List<Instrument>();

            result.Add(new Instrument()
            {
                BandMemberName = "Liam",
                Detail = "",
                Left = 800,
                Top = 250,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Bass.png",
                Text = "Bass Guitar",
                SelectedInstrument = "Bass"

            });

            result.Add(new Instrument()
            {
                BandMemberName = "Liam",
                Detail = "",
                Left = 800,
                Top = 120,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Speaker-01.png",
                Text = "Bass amp",
                SelectedInstrument = "Speaker-01"

            });

            result.Add(new Instrument()
            {
                BandMemberName = "Liam",
                Detail = "",
                Left = 800,
                Top = 350,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Monitor.png",
                Text = "Monitor",
                SelectedInstrument = "Monitor"

            });

            result.Add(new Instrument()
            {
                BandMemberName = "Liam",
                Detail = "",
                Left = 590,
                Top = 170,
                Src = "https://stage-plan.com/Content/Stageplan/Images/Power-plug.png",
                Text = "Power point",
                SelectedInstrument = "PowerPoint"

            });

            result.Add(new Instrument()
            {
                BandMemberName = "Liam",
                Detail = "",
                Left = 690,
                Top = 250,
                Src = "https://stage-plan.com/Content/Stageplan/Images/DI-Box.png",
                Text = "DI Box",
                SelectedInstrument = "DI-Box"

            });

            return result;
        }
    }
}
