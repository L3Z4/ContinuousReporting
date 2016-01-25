$(document).ready(function () {
    Chart.defaults.global.scaleBeginAtZero = true;
    if ($('#myChart').length) {
        var canvas = $("#myChart");
        var ctx = canvas.get(0).getContext("2d");
        console.log(ctx);
        var xValue = canvas.data('x-values');
        var yValue = canvas.data('y-values');

        var data = {
            labels:
                xValue.split(','),
                datasets: [
                    {
                        fillColor: "rgba(151,187,205,0.2)",
                        strokeColor: "rgba(151,187,205,1)",
                        pointColor: "rgba(151,187,205,1)",
                        pointStrokeColor: "#fff",
                        pointHighlightFill: "#fff",
                        pointHighlightStroke: "rgba(151,187,205,1)",
                        data: yValue.split(',')
                    }
                ]
        }

        var myNewChart = new Chart(ctx).Radar(data);
        console.log(myNewChart);
    }

    if ($('#MainChart').length) {
        displayChart("MainChart");
    }
});

function displayChart(buildName) {
    $('.moduleCoverageChart').each(function() {
        $(this).hide();
    });
    var canvas = $('#' + buildName);
    canvas.parent().show();
    console.log(canvas);
    var xValue = canvas.data('x-values');
    var yValue = canvas.data('y-values').toString();

    var context = canvas.get(0).getContext("2d");
    var chartData = {
        labels: xValue.indexOf(',') != -1 ? xValue.split(',') : '\'' + xValue +'\'',
        datasets: [
            {
                fillColor: "rgba(151,187,205,0.2)",
                strokeColor: "rgba(151,187,205,1)",
                pointColor: "rgba(151,187,205,1)",
                pointStrokeColor: "#fff",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(151,187,205,1)",
                data: yValue.indexOf(',') != -1 ? yValue.split(',') : yValue
            }]
    };
    new Chart(context).Line(chartData);
    
}