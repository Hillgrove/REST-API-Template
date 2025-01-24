Vue.createApp({
    data() {
        return {
            tools: [
                { name: "Vue.js", url: "https://vuejs.org/" },
                { name: "React.js", url: "https://reactjs.org/" },
                { name: "Angular", url: "https://angular.io/" },
            ],

            newToolName: '',
            newToolUrl: ''
        }
    },

    methods: {
        addTool() {
            if (this.newToolName && this.newToolUrl) {
                this.tools.push({ name: this.newToolName, url: this.newToolUrl });
                this.newToolName = '';
                this.newToolUrl = '';
            }
        },

        deleteTool(index) {
            this.tools.splice(index, 1);
        }
    }

}).mount("#app");