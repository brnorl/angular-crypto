import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';


@Component({
  selector: 'app-crypto',
  templateUrl: './crypto.component.html',
  styleUrls: ['./crypto.component.css']
})
export class CryptoComponent implements OnInit {
  options: any;
  isLoading: any;
  data: any;
  dates: any = [];
  prices: any = [];
  private timer: any;

  constructor(private http: HttpClient) { }

  async ngOnInit(): Promise<any> {
    this.data = await this.http.get('https://localhost:5001/Currency').toPromise();
    this.setGraph();
    for (let key in this.data) {
      this.dates[key] = this.data[key].idate.slice(0, 10) + '\n' + this.data[key].idate.slice(11, 20);
      this.prices[key] = (this.data[key].price);
    }
    this.timer = setInterval(() => {
      this.data = this.http.get('https://localhost:5001/Currency').toPromise();
      for (let key in this.data) {
        this.dates[key] = this.data[key].idate.slice(0, 10) + '\n' + this.data[key].idate.slice(11, 20);
        this.prices[key] = (this.data[key].price);
      }
      this.setGraph();
    }, 10000);//minutely
  }


  setGraph() {
    this.isLoading = false;
    this.options = {
      tooltip: {
        trigger: 'axis',
        axisPointer: {
          type: 'cross',
          label: {
            backgroundColor: '#6a7985'
          }
        }
      },
      legend: {
        data: ['BTC']
      },
      grid: {
        left: '3%',
        right: '4%',
        bottom: '3%',
        containLabel: true
      },
      xAxis: [
        {
          type: 'category',
          boundaryGap: true,
          data: this.dates
        }
      ],
      yAxis: [
        {
          type: 'value'
        }
      ],
      series: [
        {
          name: 'BTC',
          type: 'line',
          stack: 'counts',
          areaStyle: { normal: {} },
          data: this.prices
        }
      ]
    };
  }
}



