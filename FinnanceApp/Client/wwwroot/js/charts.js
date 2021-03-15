function GenerateMonthChart(mLabels, mValue) {
    var ctx = document.getElementById('monthChart').getContext('2d');
    var chart = new Chart(ctx, {
        // The type of chart we want to create
        type: 'line',

        // The data for our dataset
        data: {
            labels: mLabels,
            datasets: [{
                lineTension: 0.3,
                backgroundColor: "rgba(78, 115, 223, 0.05)",
                borderColor: "rgba(78, 115, 223, 1)",
                pointRadius: 3,
                pointBackgroundColor: "rgba(78, 115, 223, 1)",
                pointBorderColor: "rgba(78, 115, 223, 1)",
                pointHoverRadius: 3,
                pointHoverBackgroundColor: "rgba(78, 115, 223, 1)",
                pointHoverBorderColor: "rgba(78, 115, 223, 1)",
                pointHitRadius: 10,
                pointBorderWidth: 2,
                data: mValue
            }]
        },
        options: {
            maintainAspectRatio: false,
            layout: {
              padding: {
                left: 10,
                right: 25,
                top: 25,
                bottom: 0
              }
            },
            scales: {
              xAxes: [{
                time: {
                  unit: 'date'
                },
                gridLines: {
                  display: false,
                  drawBorder: false
                },
                ticks: {
                  maxTicksLimit: 7
                }
              }],
              yAxes: [{
                ticks: {
                  maxTicksLimit: 5,
                  padding: 10,
                },
                gridLines: {
                  color: "rgb(234, 236, 244)",
                  zeroLineColor: "rgb(234, 236, 244)",
                  drawBorder: false,
                  borderDash: [2],
                  zeroLineBorderDash: [2]
                }
              }],
            },
            legend: {
              display: false,
            },
            tooltips: {
              backgroundColor: "rgb(255,255,255)",
              bodyFontColor: "#858796",
              titleMarginBottom: 10,
              titleFontColor: '#6e707e',
              titleFontSize: 14,
              borderColor: '#dddfeb',
              borderWidth: 1,
              xPadding: 15,
              yPadding: 15,
              displayColors: false,
              intersect: false,
              mode: 'index',
              caretPadding: 10,
            }
          }

    });
}
function GeneratePersonChart(pLabels,pValue){
  console.log("test");
  var ctx = document.getElementById('donutChart').getContext('2d');
  var myDoughnutChart = new Chart(ctx, {
   type: 'doughnut',
    data: {
        labels: pLabels,
        datasets: [{
            data: pValue,
            backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc','#4e73df', '#1cc88a','#4e73df', '#1cc88a'],
            hoverBackgroundColor: ['#2e59d9', '#17a673', '#2c9faf','#2e59d9', '#17a673', '#2c9faf','#2e59d9'],
            hoverBorderColor: "rgba(234, 236, 244, 1)",
        }],
    },
    options: {
      maintainAspectRatio: false,
      tooltips: {
        backgroundColor: "rgb(255,255,255)",
        bodyFontColor: "#858796",
        borderColor: '#dddfeb',
        borderWidth: 1,
        xPadding: 20,
        yPadding: 20,
        displayColors: false,
        caretPadding: 10,
      },
      legend: {
        display: true,
        position: 'bottom',
        labels:{
          fontSize:12,
          padding:20
        }
      },
      cutoutPercentage: 80,
    }
});
myDoughnutChart.resize()

}