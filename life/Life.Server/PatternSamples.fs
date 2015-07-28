namespace Life.Server

module PatternSamples = 
    let deadSparkCoil = {
        Pattern.Name = "Dead Spark Coil"
        RLE =
            "#N Dead spark coil\r\n\
            #C An 18-cell still life.\r\n\
            #C http://www.conwaylife.com/wiki/index.php?title=Dead_spark_coil\r\n\
            x = 7, y = 5, rule = B3/S23\r\n\
            2o3b2o$obobobo$2bobo2b$obobobo$2o3b2o!"
    }

    let acorn = {
        Pattern.Name = "Acorn"
        RLE =
            "#N Acorn\r\n\
             #O Charles Corderman\r\n\
             #C A methuselah with lifespan 5206.\r\n\
             #C www.conwaylife.com/wiki/index.php?title=Acorn\r\n\
             x = 7, y = 3, rule = B3/S23\r\n\
             bo5b$3bo3b$2o2b3o!"
    }

    let all = [deadSparkCoil; acorn]